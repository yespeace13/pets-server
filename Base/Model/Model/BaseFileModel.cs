using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetsServer.Base.BaseFile.Model;

public class BaseFileModel<T>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("parent_id")]
    public int ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public T Entity { get; set; }

    [Column("path")]
    public string Path { get; set; }
}
