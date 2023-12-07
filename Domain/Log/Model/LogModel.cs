using PetsServer.Auth.Authorization.Model;
using PetsServer.Domain.Locality.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Log.Model
{
    [Table("organization")]
    public class LogModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public UserModel User { get; set; }

        [Column("action_date")]
        public DateTime ActionDate { get; set; }

        [Column("entity")]
        public Entities Entity { get; set; }

        [Column("id_object")]
        public int IdObject { get; set; }
        [Column("id_file")]
        public int IdFile { get; set; }

    }
}
