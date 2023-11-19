using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Auth.Authorization.Model;

[Table("role")]
public class RoleModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id"), Key]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; }

    public List<EntityPossibilities> Possibilities { get; set; }


    public RoleModel() { }

    public RoleModel(int id, string name, List<EntityPossibilities> possibilities)
    {
        Id = id;
        Name = name;
        Possibilities = possibilities;
    }
}
