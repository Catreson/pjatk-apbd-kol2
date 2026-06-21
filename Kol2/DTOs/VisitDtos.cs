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
public class CustomerCreateDto
{
    [Required]
    public CustomerInfoDto Customer { get; set; } = null!;
    [Required, MinLength(1)]
    public List<PurchaseCreateDto> Purchases { get; set; } = [];
}

public class CustomerInfoDto
{
    [Required, MaxLength(50)]
    public string FirstName { get; set; } = null!;
    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;
    [MaxLength(100)]
    public string? PhoneNumber { get; set; }
}

public class PurchaseCreateDto
{
    [Range(1, int.MaxValue)]
    public int SeatNumber { get; set; }
    [Required]
    public string ConcertName { get; set; } = null!;
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
}