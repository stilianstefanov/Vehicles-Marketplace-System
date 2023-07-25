namespace ThinkElectric.Web.ViewModels.Scooter;

using Product;

public class ScooterDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string Battery { get; set; } = null!;

    public string ScooterType { get; set; } = null!;

    public string EngineType { get; set; } = null!;

    public string BrakesType { get; set; } = null!;

    public int Range { get; set; }

    public int TopSpeed { get; set; }

    public int Weight { get; set; }

    public int MaxLoad { get; set; }

    public int TireSize { get; set; }

    public int ChargingTime { get; set; }

    public int EnginePower { get; set; }

    public string ProductId { get; set; } = null!;

    public ProductViewModel Product { get; set; } = null!;
}
