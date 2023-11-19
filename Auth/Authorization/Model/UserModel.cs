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

    public UserModel() { }

    public UserModel(int id, string log, string pass, LocalityModel locality, OrganizationModel organization, RoleModel? role)
    {
        Id = id;
        Login = log;
        Password = pass;
        Locality = locality;
        Organization = organization;
        Role = role;
    }
}
