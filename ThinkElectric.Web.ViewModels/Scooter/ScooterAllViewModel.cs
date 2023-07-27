namespace ThinkElectric.Web.ViewModels.Scooter;

using System.ComponentModel.DataAnnotations;
using Product;

public class ScooterAllViewModel
{
    public string Id { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Color { get; set; } = null!;

    [Display(Name = "Scooter Type")]
    public string ScooterType { get; set; } = null!;

    [Display(Name = "Engine Type")]
    public string EngineType { get; set; } = null!;

    [Display(Name = "Brakes Type")]
    public string BrakesType { get; set; } = null!;

    public int Range { get; set; }

    public int MaxSpeed { get; set; }

    public int EnginePower { get; set; }

    public ProductViewModel Product { get; set; } = null!;
}
