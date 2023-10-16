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


    [Column("category_animal")]
    public string Category { get; set; }

    [Column("sex_animal")]
    public bool? Sex { get; set; }

    [Column("breed_animal")]
    public string? Breed { get; set; }

    [Column("size_animal")]
    public double? Size { get; set; }

    [Column("wool_animal")]
    public string? Wool { get; set; }

    [Column("color_animal")]
    public string? Color { get; set; }

    [Column("ears_animal")]
    public string? Ears { get; set; }

    [Column("tail_animal")]
    public string? Tail { get; set; }

    [Column("specialSigns_animal")]
    public string? SpecialSigns { get; set; }

    [Column("identificationLabel_animal")]
    public string? IdentificationLabel { get; set; }

    [Column("chipNumber_animal")]
    public string? ChipNumber { get; set; }

    public AnimalModel() { }
    public AnimalModel(
            string category, bool sex, string breed, double size, string wool, string color,
            string ears, string tail, string specialSigns, string identificationLabel, string chipNumber)
    {
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
