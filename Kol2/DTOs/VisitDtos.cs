using System.ComponentModel.DataAnnotations;

namespace Kol2.DTOs;

// ── GET /api/customers/{id}/purchases ──────────────────────────────────────
public class VisitDto
{
    public DateTime Date { get; set; }
    public ClientDto Client { get; set; } = null!;
    public MechanicDto Mechanic { get; set; } = null!;
    public List<VisitServiceDto> VisitServices { get; set; } = [];

}

public class ClientDto
{
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;
    [MaxLength(100)]
    public DateTime DateOfBirth { get; set; }
}

public class MechanicDto
{
    public int MechanicId { get; set; }
    [Required, MaxLength(14)]
    public string LicenceNumber { get; set; } = null!;

}

public class VisitServiceDto
{
    public string Name { get; set; } = null!;
    public decimal ServiceFee { get; set; }
}

// ── POST /api/customers ────────────────────────────────────────────────────
public class VisitCreateDto
{
    [Required]
    public int ClientId { get; set;}
    [Required]
    public string MechanicLicenceNumber { get; set; } = null!;
    [Required, MinLength(1)]
    public List<ServiceCreateDto> Services { get; set; } = [];
}

public class VisitServiceCreateDto
{
    [Range(0.01, double.MaxValue)]
    public decimal BaseFee { get; set; }
}

public class ServiceCreateDto
{
    [Required]
    public string ServiceName { get; set; } = null!;
    [Range(0.01, double.MaxValue)]
    public decimal ServiceFee { get; set; }
}