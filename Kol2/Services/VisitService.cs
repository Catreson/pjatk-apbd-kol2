using Kol2.Data;
using Kol2.DTOs;
using Kol2.Entities;
using Kol2.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Kol2.Services;

public class VisitService : IVisitService
{
    private readonly AppDbContext _db;
    public VisitService(AppDbContext db) => _db = db;

    // ── GET ────────────────────────────────────────────────────────────────
    public async Task<VisitDto> GetVisitAsync(int visitId)
    {
        var dto = await _db.Visits
            .Where(c => c.VisitId == visitId)
            .Select(c => new VisitDto
            {
                Date = c.Date,
                Client  = new ClientDto
                {
                    FirstName = c.Clients.FirstName,
                    LastName = c.Clients.LastName,
                    DateOfBirth = c.Clients.DateOfBirth
                },
                Mechanic = new MechanicDto
                {
                    MechanicId = c.MechanicId,
                    LicenceNumber = c.Mechanics.LicenceNumber
                },
                VisitServices = c.VisitServices.Select(pt => new VisitServiceDto
                {
                    Name  = pt.Services.Name,
                    ServiceFee = pt.ServiceFee,
                
                }).ToList()
            })
            .FirstOrDefaultAsync();

        if (dto is null)
            throw new NotFoundException($"Visit {visitId} not found.");

        return dto;
    }

    //── POST ───────────────────────────────────────────────────────────────
    public async Task CreateAsync(VisitCreateDto dto)
    {
        using var transaction = await _db.Database.BeginTransactionAsync();
        try
        {
            // 1. Customer must not already exist
            if (!await _db.Clients.AnyAsync(c => c.ClientId == dto.ClientId))
                    throw new NotFoundException($"Client '{dto.ClientId}' not found.");
            if (!await _db.Mechanics.AnyAsync(c => c.LicenceNumber == dto.MechanicLicenceNumber))
                    throw new NotFoundException($"Mechanic '{dto.MechanicLicenceNumber}' not found.");
            
            var mechanicId = await _db.Mechanics.FirstAsync(c => c.LicenceNumber == dto.MechanicLicenceNumber);
            foreach (var serviceName in dto.Services.Select(p => p.ServiceName).Distinct())
            {
                if (!await _db.Services.AnyAsync(c => c.Name == serviceName))
                    throw new NotFoundException($"Service '{serviceName}' not found.");
            }
            
            var visit = new Visit
            {
                ClientId = dto.ClientId,
                MechanicId = mechanicId.MechanicId,
            };
            _db.Visits.Add(visit);
            // 4. For each purchase: Ticket → TicketConcert → PurchasedTicket
            //    Using navigation properties so EF Core resolves all FKs in one SaveChanges.
            foreach (var service in dto.Services)
            {
                var serviceReal = await _db.Services.FirstAsync(c => c.Name == service.ServiceName);

                _db.VisitServices.Add(new VisitServicer
                {
                    ServiceId = serviceReal.ServiceId,
                    Visits = visit,
                    ServiceFee = service.ServiceFee,
                });
            }

            await _db.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}