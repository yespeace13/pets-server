using PetsServer.BaseFile.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Act.Model;

[Table("act_file")]
public class ActPhoto : BaseFileModel<ActModel>
{

}
