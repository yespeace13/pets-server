using SupportLibrary.Model.Locality;
using SupportLibrary.Model.Organization;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_5.Organization.Model
{
    [Table("Organizations")]
    public class OrganizationModel
    {
        public int Id { get; set; }
        public string NameOrganization { get; set; }
        public string Inn { get; set; }
        public string KPP { get; set; }
        public string Address { get; set; }
        public TypeOrganizationModel TypeOrganization { get; set; }
        
        public LegalTypeModel LegalType { get; set; }

        public LocalityModel Locality { get; set; }

        public OrganizationModel() { }
        public OrganizationModel(int id, string name, string inn, string kpp, string address,
            TypeOrganizationModel typeOrganizations, LegalTypeModel legalType, LocalityModel locality)
        {
            Id = id;
            NameOrganization = name;
            Inn = inn;
            KPP = kpp;
            Address = address;
            TypeOrganization = typeOrganizations;
            LegalType = legalType;
            Locality = locality;
        }
    }
}
