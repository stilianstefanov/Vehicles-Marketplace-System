﻿namespace ThinkElectric.Web.ViewModels.Product;

using Review;

public class ProductDetailsViewModel
{
    public string Name { get; set; } = null!;

    public string Price { get; set; } = null!;

    public int Quantity { get; set; }

    public string CompanyId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string ImageId { get; set; } = null!;

    public ImageViewModel Image { get; set; } = null!;

    public string Rating => Reviews.Any() ? Reviews.Average(r => r.Rating).ToString("f1") : "0";

    public IEnumerable<ReviewViewModel> Reviews { get; set; } = new HashSet<ReviewViewModel>();
}
