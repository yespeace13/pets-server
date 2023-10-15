using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Organization.Model
{
    [Table("legal_type")]
    public class LegalTypeModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        public LegalTypeModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
