using PetsServer.Domain.Locality.Model;
using PetsServer.Domain.Organization.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Auth.Authorization.Model;

[Table("user")]
public class UserModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("login")]
    public string Login { get; set; }

    [Column("password")]
    public string Password { get; set; }

    [Column("locality_id")]
    public int LocalityId { get; set; }

    [ForeignKey(nameof(LocalityId))]
    public LocalityModel Locality { get; set; }

    [Column("organization_id")]
    public int OrganizationId { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    public OrganizationModel Organization { get; set; }

    [Column("role_id")]
    public int? RoleId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public RoleModel? Role { get; set; }

    [Column("last_name")]
    public string LastName { get; set; }

    [Column("firts_name")]
    public string FirstName { get; set; }
    [Column("middle_name")]
    public string? MiddleName { get; set; }
    [Column("email")]
    public string? Email { get; set; }
    [Column("department")]
    public string? Department { get; set; }
    [Column("position")]
    public string? Position { get; set; }
    [Column("Phone")]
    public string Phone { get; set; }

    public UserModel() { }
}
