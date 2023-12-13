using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Plan.Model;

[Table("plan_status")]

public class PlanStatusModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("status_name")]
    public string StatusName { get; set; }

    public PlanStatusModel() { }

    public PlanStatusModel(int id, string statusName)
    {
        Id = id;
        StatusName = statusName;
    }
}