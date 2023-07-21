namespace ThinkElectric.Data.Models;

using System.ComponentModel.DataAnnotations;
using Enums.Bike;
using static Common.EntityValidationConstants.Bike;

public class Bike
{
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
    [MaxLength(FrameMaterialMaxLength)]
    public string FrameMaterial { get; set; } = null!;

    [Required]
    [MaxLength(BatteryMaxLength)]
    public string Battery { get; set; } = null!;

    public BikeType Type { get; set; }

    public BikeFrameType FrameType { get; set; }

    public BikeSuspensionType SuspensionType { get; set; }

    public BikeBrakesType BrakesType { get; set; }

    public BikeEngineType EngineType { get; set; }

    public int FrameSize { get; set; }

    public int GearsCount { get; set; }

    public int Range { get; set; }

    public int TopSpeed { get; set; }

    public int Weight { get; set; }

    public int MaxLoad { get; set; }

    public int WheelSize { get; set; }

    public int ChargingTime { get; set; }

    public int EnginePower { get; set; }


    public Guid ProductId { get; set; }

    public Product Product { get; set; } = null!;
}
