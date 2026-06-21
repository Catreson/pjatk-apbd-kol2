using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kol2.Entities;

[Table("Mechanic")]
public class Mechanic
{
    [Key]
    public int MechanicId { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    [MaxLength(14)]
    public string LicenceNumber { get; set; } = null!;

    public ICollection<Visit> Visits { get; set; } = [];
}