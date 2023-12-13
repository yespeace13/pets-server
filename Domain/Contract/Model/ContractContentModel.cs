using PetsServer.Domain.Locality.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Contract.Model;

[Table("contract_content")]
public class ContractContentModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("contract_id")]
    public int ContractId { get; set; }

    [ForeignKey(nameof(ContractId))]
    public ContractModel? Contract { get; set; } = null;

    [Column("locality_id")]
    public int LocalityId { get; set; }

    [ForeignKey(nameof(LocalityId))]
    public LocalityModel? Locality { get; set; } = null;
}
