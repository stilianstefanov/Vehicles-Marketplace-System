namespace ThinkElectric.Web.ViewModels.Company
{
    public class CompanyAllViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Website { get; set; }

        public string FoundedDate { get; set; } = null!;

        public string ImageId { get; set; } = null!;

        public ImageViewModel Image { get; set; } = null!;
    }
}
