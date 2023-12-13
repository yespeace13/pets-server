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

    public IQueryable<EntityPossibilities> Possibilities { get; set; }
}
