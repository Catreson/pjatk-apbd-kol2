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

    // ── POST ───────────────────────────────────────────────────────────────
    // public async Task CreateAsync(CustomerCreateDto dto)
    // {
    //     using var transaction = await _db.Database.BeginTransactionAsync();
    //     try
    //     {
    //         // 1. Customer must not already exist
    //         if (await _db.Customers.AnyAsync(c =>
    //                 c.FirstName == dto.Customer.FirstName &&
    //                 c.LastName  == dto.Customer.LastName))
    //             throw new ConflictException(
    //                 $"Customer {dto.Customer.FirstName} {dto.Customer.LastName} already exists.");

    //         // 2. Validate concerts + ticket limit per concert
    //         foreach (var concertName in dto.Purchases.Select(p => p.ConcertName).Distinct())
    //         {
    //             if (!await _db.Concerts.AnyAsync(c => c.Name == concertName))
    //                 throw new NotFoundException($"Concert '{concertName}' not found.");

    //             if (dto.Purchases.Count(p => p.ConcertName == concertName) > 5)
    //                 throw new BadRequestException(
    //                     $"Cannot buy more than 5 tickets for concert '{concertName}'.");
    //         }

    //         // 3. Create customer
    //         var customer = new Customer
    //         {
    //             FirstName   = dto.Customer.FirstName,
    //             LastName    = dto.Customer.LastName,
    //             PhoneNumber = dto.Customer.PhoneNumber
    //         };
    //         _db.Customers.Add(customer);

    //         // 4. For each purchase: Ticket → TicketConcert → PurchasedTicket
    //         //    Using navigation properties so EF Core resolves all FKs in one SaveChanges.
    //         foreach (var purchase in dto.Purchases)
    //         {
    //             var concert = await _db.Concerts.FirstAsync(c => c.Name == purchase.ConcertName);

    //             _db.PurchasedTickets.Add(new PurchasedTicket
    //             {
    //                 TicketConcert = new TicketConcert
    //                 {
    //                     Ticket = new Ticket
    //                     {
    //                         SerialNumber = Guid.NewGuid().ToString("N")[..16].ToUpper(),
    //                         SeatNumber   = purchase.SeatNumber
    //                     },
    //                     ConcertId = concert.ConcertId,
    //                     Price     = purchase.Price
    //                 },
    //                 Customer     = customer,
    //                 PurchaseDate = DateTime.UtcNow
    //             });
    //         }

    //         await _db.SaveChangesAsync();
    //         await transaction.CommitAsync();
    //     }
    //     catch
    //     {
    //         await transaction.RollbackAsync();
    //         throw;
    //     }
    // }
}