using PetsServer.BaseFile.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Contract.Model;

[Table("contract_file")]
public class ContractPhoto : BaseFileModel<ContractModel>
{

}
