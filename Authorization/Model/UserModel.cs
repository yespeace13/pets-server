using IS_5.Organization.Model;
using PetsServer.Locality.Model;

namespace PetsServer.Authorization.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public LocalityModel Locality { get; set; }
        public OrganizationModel Organization { get; set; }
        public Role Privilege { get; set; }

        public UserModel(int id, string log, string pass, LocalityModel locality, OrganizationModel organization, Role privilege) 
        {
            Id = id;
            Login = log;
            Password = pass;
            Locality = locality;
            Organization = organization;
            Privilege = privilege;
        }
    }
}