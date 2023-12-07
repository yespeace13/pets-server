﻿using PetsServer.Domain.Locality.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PetsServer.Domain.Organization.Model
{
    [Table("organization")]
    public class OrganizationModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name_organization")]
        public string NameOrganization { get; set; }

        [Column("inn")]
        public string INN { get; set; }

        [Column("kpp")]
        public string? KPP { get; set; }

        [Column("address")]
        public string Address { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("email")]
        public string Email { get; set; }

        [Column(name: "type_organization_id")]
        public int TypeOrganizationId { get; set; }

        [ForeignKey(nameof(TypeOrganizationId))]
        public TypeOrganizationModel TypeOrganization { get; set; }


        [Column(name: "legal_type_id")]
        public int LegalTypeId { get; set; }
        [ForeignKey(nameof(LegalTypeId))]
        public LegalTypeModel LegalType { get; set; }


        [Column(name: "locality_id")]
        public int LocalityId { get; set; }
        [ForeignKey(nameof(LocalityId))]
        public LocalityModel Locality { get; set; }

        public OrganizationModel() { }
        public OrganizationModel(int id, string name, string inn, string kpp, string address, string phone, string email,
            TypeOrganizationModel typeOrganizations, LegalTypeModel legalType, LocalityModel locality)
        {
            Id = id;
            NameOrganization = name;
            INN = inn;
            KPP = kpp;
            Address = address;
            Phone = phone;
            Email = email;
            TypeOrganizationId = typeOrganizations.Id;
            LegalTypeId = legalType.Id;
            LocalityId = locality.Id;
        }
    }
}
