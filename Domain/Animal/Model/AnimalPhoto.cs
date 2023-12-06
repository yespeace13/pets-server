using PetsServer.BaseFile.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Animal.Model;

[Table("animal_file")]
public class AnimalPhoto : BaseFileModel<AnimalModel>
{

}
