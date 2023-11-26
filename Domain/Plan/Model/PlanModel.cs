using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Plan.Model;

[Table("plan")]
public class PlanModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("month")]
    public int Month { get; set; }

    [Column("year")]
    public int Year { get; set; }

    public List<PlanContentModel> PlanContent { get; set; }

    public PlanModel() { }
    public PlanModel(
            int id, int month, int year)
    {
        Id = id;
        Month = month;
        Year = year;
    }
}
