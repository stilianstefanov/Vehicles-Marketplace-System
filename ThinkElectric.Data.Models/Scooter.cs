namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ThinkElectric.Data.Models.Enums.Scooter;
using static Common.EntityValidationConstants.Scooter;

public class Scooter
{
    public Scooter()
    {
        Id = Guid.NewGuid();
    }

    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(BrandMaxLength)]
    public string Brand { get; set; } = null!;

    [Required]
    [MaxLength(ModelMaxLength)]
    public string Model { get; set; } = null!;

    [Required]
    [MaxLength(ColorMaxLength)]
    public string Color { get; set; } = null!;

    [Required]
    [MaxLength(BatteryMaxLength)]
    public string Battery { get; set; } = null!;

    public ScooterType Type { get; set; }

    public ScooterEngineType EngineType { get; set; }

    public ScooterBrakesType BrakesType { get; set; }

    public int Range { get; set; }

    public int TopSpeed { get; set; }

    public int Weight { get; set; }

    public int MaxLoad { get; set; }

    public int TireSize { get; set; }

    public int ChargingTime { get; set; }

    public int EnginePower { get; set; }


    [ForeignKey(nameof(Product))]
    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;
}
