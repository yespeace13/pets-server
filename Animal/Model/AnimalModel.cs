using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetsServer.Animal.Model;

[Table("animal")]
public class AnimalModel
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    [Key]
    public int Id { get; set; }


    [Column("category")]
    public string Category { get; set; }

    [Column("sex")]
    public bool? Sex { get; set; }

    [Column("breed")]
    public string? Breed { get; set; }

    [Column("size")]
    public double? Size { get; set; }

    [Column("wool")]
    public string? Wool { get; set; }

    [Column("color")]
    public string? Color { get; set; }

    [Column("ears")]
    public string? Ears { get; set; }

    [Column("tail")]
    public string? Tail { get; set; }

    [Column("special_signs")]
    public string? SpecialSigns { get; set; }

    [Column("identification_label")]
    public string? IdentificationLabel { get; set; }

    [Column("chip_number")]
    public string? ChipNumber { get; set; }

    public AnimalModel() { }
    public AnimalModel(
            int id, string category, bool sex, string breed, double size, string wool, string color,
            string ears, string tail, string specialSigns, string identificationLabel, string chipNumber)
    {
        Id = id;
        Category = category;
        Sex = sex;
        Breed = breed;
        Size = size;
        Wool = wool;
        Color = color;
        Ears = ears;
        Tail = tail;
        SpecialSigns = specialSigns;
        IdentificationLabel = identificationLabel;
        ChipNumber = chipNumber;
    }
}
