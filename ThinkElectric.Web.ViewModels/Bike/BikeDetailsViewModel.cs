namespace ThinkElectric.Web.ViewModels.Bike;

using Product;

public class BikeDetailsViewModel
{
    public string Id { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Color { get; set; } = null!;

    public string FrameMaterial { get; set; } = null!;

    public string Battery { get; set; } = null!;

    public string BikeType { get; set; } = null!;

    public string FrameType { get; set; } = null!;

    public string SuspensionType { get; set; } = null!;

    public string BrakesType { get; set; } = null!;

    public string EngineType { get; set; } = null!;

    public int FrameSize { get; set; }

    public int GearsCount { get; set; }

    public int Range { get; set; }

    public int TopSpeed { get; set; }

    public int Weight { get; set; }

    public int MaxLoad { get; set; }

    public int WheelSize { get; set; }

    public int ChargingTime { get; set; }

    public int EnginePower { get; set; }

    public string ProductId { get; set; } = null!;

    public ProductDetailsViewModel Product { get; set; } = null!;
}
