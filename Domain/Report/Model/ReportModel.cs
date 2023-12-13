using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Report.Model;

[Table("report")]
public class ReportModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("number")]
    public int Number { get; set; }

    [Column("date_start")]
    public DateOnly DateStart { get; set; }

    [Column("date_end")]
    public DateOnly DateEnd { get; set; }
    [Column("status_id")]
    public int StatusId { get; set; }

    [ForeignKey(nameof(StatusId))]
    public ReportStatusModel Status { get; set; }
    [Column("date_status")]
    public DateTime DateStatus { get; set; }

    public ICollection<ReportContentModel>? ReportContent { get; set; }

    public ReportModel() { }
    public ReportModel(
            int id, int number, DateOnly dateStart, DateOnly dateEnd)
    {
        Id = id;
        Number = number;
        DateStart = dateStart;
        DateEnd = dateEnd;
    }
}
