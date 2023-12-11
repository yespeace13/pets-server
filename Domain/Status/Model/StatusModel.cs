using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Status.Model;

[Table("status")]

public class StatusModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("status_name")]
    public string StatusName { get; set; }

    public StatusModel() { }

    public StatusModel(int id, string statusName)
    {
        Id = id;
        StatusName = statusName;
    }
}