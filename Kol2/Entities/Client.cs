using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kol2.Entities;

[Table("Client")]
public class Client
{
    [Key]
    public int ClientId { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }

    public ICollection<Visit> Visits { get; set; } = [];
}