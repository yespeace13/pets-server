using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Organization.Model
{
    [Table("LegalTypeModel")]
    public class LegalTypeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LegalTypeModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
