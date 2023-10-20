using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetsServer.Authorization.Model
{
    [Table("entity_possibilities")]
    public class EntityPossibilities
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id"), Key]
        public int Id { get; set; }

        [Column("entity")]
        public Entities Entity { get; set; }

        [Column("possibility")]
        public Possibilities Possibility { get; set; }

        [Column("restriction")]
        public Restrictions Restriction { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        public RoleModel Role { get; set; }

        public EntityPossibilities() { }

        public EntityPossibilities(int id, Entities entity, Possibilities possibility, Restrictions restriction, RoleModel role)
        {
            Id = id;
            Entity = entity;
            Possibility = possibility;
            Restriction = restriction;
            RoleId = role.Id;
        }

    }
}
