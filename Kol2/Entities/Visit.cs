using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kol2.Entities;

[Table("Visit")]
public class Visit
{
    [Key]
    public int VisitId { get; set; }

    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }
    [ForeignKey(nameof(Mechanic))]
    public int MechanicId { get; set; }
    public DateTime Date { get; set; }
    public Client Clients { get; set; } = null!;
    public Mechanic Mechanics { get; set; } = null!;
    public ICollection<VisitServicer> VisitServices { get; set; } = [];
}