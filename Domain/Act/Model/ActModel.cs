using PetsServer.Domain.Animal.Model;
using PetsServer.Domain.Locality.Model;
using PetsServer.Domain.Organization.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Act.Model;

[Table("act")]

public class ActModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("executor_id")]
    public int ExecutorId { get; set; }

    [ForeignKey(nameof(ExecutorId))]
    public OrganizationModel Executor { get; set; }
    [Column("locality_id")]
    public int LocalityId { get; set; }

    [ForeignKey(nameof(LocalityId))]
    public LocalityModel Locality { get; set; }

    [Column("date_of_capture")]
    //TODO поменять
    public DateTime DateOfCapture { get; set; }

    public List<AnimalModel> Animal { get; set; }

    public ActModel(
            int id, OrganizationModel executor, LocalityModel locality, DateTime date)
    {
        Id = id;
        ExecutorId = executor.Id;
        LocalityId = locality.Id;
        DateOfCapture = date;
    }

    public ActModel() { }

}
