using PetsServer.Domain.Locality.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Report.Model;

[Table("report_content")]
public class ReportContentModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("total_cost")]
    public decimal TotalCost { get; set; }

    [Column("locality_id")]
    public int LocalityId { get; set; }

    [ForeignKey(nameof(LocalityId))]
    public LocalityModel Locality { get; set; }

    [Column("number_of_animals")]
    public int NumberOfAnimals { get; set; }

    [Column("report_id")]
    public int ReportId { get; set; }

    [ForeignKey(nameof(ReportId))]
    public ReportModel Report { get; set; }
}
