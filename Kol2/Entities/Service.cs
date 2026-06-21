using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kol2.Entities;

[Table("Service")]
public class Service
{
    [Key]
    public int ServiceId { get; set; }
    [MaxLength(100)]
    public required string Name { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public int BaseFee { get; set; }

    public ICollection<VisitServicer> Visits { get; set; } = [];
}