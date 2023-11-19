using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Locality.Model
{
    [Table("locality")]
    public class LocalityModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        public LocalityModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}