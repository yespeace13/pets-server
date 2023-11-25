using ModelLibrary.Model.Plan;
using PetsServer.Domain.Organization.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Plan.Model;

[Table("plan")]
public class PlanContentModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("number")]
    public int Number { get; set; }

    [Column("month")]
    public int Month { get; set; }

    [Column("year")]
    public int Year { get; set; }

    public List<PlanContentModel> PlanContent { get; set; }

    public PlanContentModel() { }
    public PlanContentModel(
            int id, int number, int month, int year)
    {
        Id = id;
        Number = number;
        Month = month;
        Year = year;
    }
}
