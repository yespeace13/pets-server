using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PetsServer.Organization.Model
{
    [Table("type_organization")]
    public class TypeOrganizationModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public TypeOrganizationModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
