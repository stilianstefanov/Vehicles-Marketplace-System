namespace ThinkElectric.Web.ViewModels.Bike;

using System.ComponentModel.DataAnnotations;
using Product;

public class BikeAllViewModel
{
    public string Id { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Color { get; set; } = null!;

    [Display(Name = "Bike Type")]
    public string BikeType { get; set; } = null!;

    [Display(Name = "Frame Type")]
    public string FrameType { get; set; } = null!;

    [Display(Name = "Suspension Type")]
    public string SuspensionType { get; set; } = null!;

    [Display(Name = "Brakes Type")]
    public string BrakesType { get; set; } = null!;

    [Display(Name = "Engine Type")]
    public string EngineType { get; set; } = null!;

    public int Range { get; set; }

    public int MaxSpeed { get; set; }

    public int EnginePower { get; set; }

    public ProductViewModel Product { get; set; } = null!;
}
