using PetsServer.Domain.Act.Model;
using PetsServer.Domain.Locality.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace PetsServer.Domain.Plan.Model;

[Table("plan_content")]
public class PlanContentModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("day")]
    public int Day { get; set; }

    [Column("locality_id")]
    public int LocalityId { get; set; }

    [ForeignKey(nameof(LocalityId))]
    public LocalityModel Locality { get; set; }

    [Column("act_id")]
    public int? ActId { get; set; }

    [ForeignKey(nameof(ActId))]
    public ActModel Act { get; set; }

    [Column("adress")]
    public string Adress { get; set; }

    [Column("check")]
    public bool Check { get; set; }

    public PlanContentModel() { }
    public PlanContentModel(
            int id, int day, LocalityModel locality, ActModel act, string adress, bool check)
    {
        Id = id;
        Day = day;
        LocalityId = locality.Id;
        ActId = act.Id;
        Adress = adress;
        Check = check;
    }
}
