using PetsServer.Base.BaseFile.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Act.Model;

[Table("animal_file")]
public class AnimalPhoto : BaseFileModel<AnimalModel>
{

}
