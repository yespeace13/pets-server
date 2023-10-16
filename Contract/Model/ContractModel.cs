using PetsServer.Organization.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Contract.Model;

[Table("contract")]
public class ContractModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Key]
    public int Id { get; set; }


    [Column("number_animal")]
    public string Number { get; set; }

    [Column("dateOfConclusion_animal")]
    public DateTime DateOfConclusion { get; set; }

    [Column("dateValidation _animal")]
    public DateTime DateValidation { get; set; }

    [Column(name: "executor_id")]
    public int ExecutorId { get; set; }

    [ForeignKey(nameof(ExecutorId))]
    public OrganizationModel Executor { get; set; }

    [Column(name: "client_id")]
    public int ClientId { get; set; }

    [ForeignKey(nameof(ClientId))]
    public OrganizationModel Client { get; set; }

    public ContractModel() { }
    public ContractModel(
            int id, string number, DateTime dateOfConclusion, DateTime dateValidation,
            OrganizationModel executors, OrganizationModel clients)
    {
        Id = id;
        Number = number;
        DateOfConclusion = dateOfConclusion;
        DateValidation = dateValidation;
        ExecutorId = executors.Id;
        ClientId = clients.Id;
    }
}
