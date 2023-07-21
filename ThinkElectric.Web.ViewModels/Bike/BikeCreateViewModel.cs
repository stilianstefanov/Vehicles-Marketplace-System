namespace ThinkElectric.Web.ViewModels.Bike;

using Product;
using System.ComponentModel.DataAnnotations;
using static Common.EntityValidationConstants.Bike;
using static Common.ErrorMessages;

public class BikeCreateViewModel
{
    public ProductCreateViewModel Product { get; set; } = null!;

    [Required]
    [StringLength(BrandMaxLength, MinimumLength = BrandMinLength)]
    public string Brand { get; set; } = null!;

    [Required]
    [StringLength(ModelMaxLength, MinimumLength = ModelMinLength)]
    public string Model { get; set; } = null!;

    [Required]
    [StringLength(ColorMaxLength, MinimumLength = ColorMinLength)]
    public string Color { get; set; } = null!;

    [Required]
    [StringLength(FrameMaterialMaxLength, MinimumLength = FrameMaterialMinLength)]
    [Display(Name = "Frame Material")]
    public string FrameMaterial { get; set; } = null!;

    [Required]
    [StringLength(BatteryMaxLength, MinimumLength = BatteryMinLength)]
    public string Battery { get; set; } = null!;

    [Range(BikeTypeMinValue, BikeTypeMaxValue, ErrorMessage = BikeTypeErrorMessage)]
    public int Type { get; set; }

    [Range(FrameTypeMinValue, FrameTypeMaxValue, ErrorMessage = BikeFrameTypeErrorMessage)]
    [Display(Name = "Frame Type")]
    public int FrameType { get; set; }

    [Range(SuspensionTypeMinValue, SuspensionTypeMaxValue, ErrorMessage = BikeSuspensionTypeErrorMessage)]
    [Display(Name = "Suspension Type")]
    public int SuspensionType { get; set; }

    [Range(BrakesTypeMinValue, BrakesTypeMaxValue, ErrorMessage = BikeBrakesTypeErrorMessage)]
    [Display(Name = "Brakes Type")]
    public int BrakesType { get; set; }

    [Range(EngineTypeMinValue, EngineTypeMaxValue, ErrorMessage = BikeEngineTypeErrorMessage)]
    [Display(Name = "Engine Type")]
    public int EngineType { get; set; }

    [Range(FrameSizeMinValue, FrameSizeMaxValue)]
    [Display(Name = "Frame Size")]
    public int FrameSize { get; set; }

    [Range(GearsCountMinValue, GearsCountMaxValue)]
    [Display(Name = "Gears Count")]
    public int GearsCount { get; set; }

    [Range(RangeMinValue, RangeMaxValue)]
    public int Range { get; set; }

    [Range(TopSpeedMinValue, TopSpeedMaxValue)]
    [Display(Name = "Top Speed")]
    public int TopSpeed { get; set; }

    [Range(WeightMinValue, WeightMaxValue)]
    public int Weight { get; set; }

    [Range(MaxLoadMinValue, MaxLoadMaxValue)]
    [Display(Name = "Max Load")]
    public int MaxLoad { get; set; }

    [Range(WheelSizeMinValue, WheelSizeMaxValue)]
    [Display(Name = "Wheel Size")]
    public int WheelSize { get; set; }

    [Range(ChargingTimeMinValue, ChargingTimeMaxValue)]
    [Display(Name = "Charging Time")]
    public int ChargingTime { get; set; }

    [Range(EnginePowerMinValue, EnginePowerMaxValue)]
    [Display(Name = "Engine Power")]
    public int EnginePower { get; set; }
}
