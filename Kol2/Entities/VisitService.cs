using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kol2.Entities;

[Table("Visit_Service")]
[PrimaryKey(nameof(ServiceId), nameof(VisitId))]
public class VisitService
{
    [ForeignKey(nameof(Service))]
    public int ServiceId { get; set; }
    [ForeignKey(nameof(Visit))]
    public int VisitId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public int ServiceFee { get; set; }


    public Service Services{ get; set; } = null!;
    public Visit Visits { get; set; } = null!;
}