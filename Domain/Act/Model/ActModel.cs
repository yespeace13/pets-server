using PetsServer.Domain.Animal.Model;
using PetsServer.Domain.Organization.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Domain.Act.Model;

[Table("act")]

public class ActModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Key]
    public int Id { get; set; }

    [Column("animal_id")]
    public int AnimalId { get; set; }

    [ForeignKey(nameof(AnimalId))]
    public AnimalModel Animal { get; set; }

    [Column("organization_id")]
    public int OrganizationId { get; set; }

    [ForeignKey(nameof(OrganizationId))]
    public OrganizationModel Organization { get; set; }

    [Column("date_of_capture")]
    public DateTime DateOfCapture { get; set; }

    public ActModel(
        int id, AnimalModel animal, OrganizationModel organization, DateTime date)
    {
        Id = id;
        AnimalId = animal.Id;
        OrganizationId = organization.Id;
        DateOfCapture = date;
    }

}
