namespace ThinkElectric.Web.ViewModels.Bike;

using System.ComponentModel.DataAnnotations;

using Enums;
using static Common.EntityValidationConstants.BikeQuery;
using static Common.GeneralApplicationConstants;

public class BikeAllQueryModel
{
    public BikeAllQueryModel()
    {
        this.CurrentPage = DefaultPage;
        this.BikesPerPage = DefaultItemsPerPage;

        Bikes = new HashSet<BikeAllViewModel>();
    }

    [MaxLength(SearchTermMaxLength)]
    [Display(Name = "Search")]
    public string? SearchTerm { get; set; }

    [Range(SortingMinValue, SortingMaxValue)]
    public BikeSorting BikeSorting { get; set; }

    [Range(BikeTypeMinValue, BikeTypeMaxValue)]
    public QueryBikeType QueryBikeType { get; set; }

    [Range(SuspensionTypeMinValue, SuspensionTypeMaxValue)]
    public QueryBikeSuspensionType QueryBikeSuspensionType { get; set; }

    [Range(FrameTypeMinValue, FrameTypeMaxValue)]
    public QueryBikeFrameType QueryBikeFrameType { get; set; }

    [Range(EngineTypeMinValue, EngineTypeMaxValue)]
    public QueryBikeEngineType QueryBikeEngineType { get; set; }

    [Range(BrakesTypeMinValue, BrakesTypeMaxValue)]
    public QueryBikeBrakesType QueryBikeBrakesType { get; set; }

    public int CurrentPage { get; set; }

    [Range(PerPageMinValue, PerPageMaxValue)]
    public int BikesPerPage { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<BikeAllViewModel> Bikes { get; set; }
}
