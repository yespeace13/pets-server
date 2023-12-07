using PetsServer.Domain.Organization.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Contract.Model;

[Table("contract")]
public class ContractModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("number")]
    public string Number { get; set; }

    [Column("date_of_conclusion")]
    public DateOnly DateOfConclusion { get; set; }

    [Column("date_valid")]
    public DateOnly DateValid { get; set; }

    [Column("executor_id")]
    public int ExecutorId { get; set; }

    [ForeignKey(nameof(ExecutorId))]
    public OrganizationModel Executor { get; set; }

    [Column("client_id")]
    public int ClientId { get; set; }

    [ForeignKey(nameof(ClientId))]
    public OrganizationModel Client { get; set; }

    public ICollection<ContractContentModel>? ContractContent { get; set; }

    public ICollection<ContractPhoto>? FIles { get; set; }

    public ContractModel() { }
    public ContractModel(
            int id, string number, DateOnly dateOfConclusion, DateOnly dateValidation,
            OrganizationModel executors, OrganizationModel clients)
    {
        Id = id;
        Number = number;
        DateOfConclusion = dateOfConclusion;
        DateValid = dateValidation;
        ExecutorId = executors.Id;
        ClientId = clients.Id;
    }
}
