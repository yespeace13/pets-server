using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Organization.Model
{
    [Table("TypeOrganizations")]
    public class TypeOrganizationModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TypeOrganizationModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
