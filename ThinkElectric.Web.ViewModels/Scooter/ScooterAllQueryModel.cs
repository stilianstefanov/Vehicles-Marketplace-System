namespace ThinkElectric.Web.ViewModels.Scooter;

using System.ComponentModel.DataAnnotations;
using Enums;
using static Common.GeneralApplicationConstants;
using static Common.EntityValidationConstants.ScooterQuery;

public class ScooterAllQueryModel
{
    public ScooterAllQueryModel()
    {
        this.CurrentPage = DefaultPage;
        this.ScootersPerPage = DefaultItemsPerPage;

        Scooters = new HashSet<ScooterAllViewModel>();
    }


    [MaxLength(SearchTermMaxLength)]
    [Display(Name = "Search")]
    public string? SearchTerm { get; set; }

    [Range(SortingMinValue, SortingMaxValue)]
    public ScooterSorting ScooterSorting { get; set; }

    [Range(ScooterTypeMinValue, ScooterTypeMaxValue)]
    public QueryScooterType QueryScooterType { get; set; }

    [Range(EngineTypeMinValue, EngineTypeMaxValue)]
    public QueryScooterEngineType QueryScooterEngineType { get; set; }

    [Range(BrakesTypeMinValue, BrakesTypeMaxValue)]
    public QueryScooterBrakesType QueryScooterBrakesType { get; set; }

    public int CurrentPage { get; set; }

    public int ScootersPerPage { get; set; }

    public int TotalPages { get; set; }

    public IEnumerable<ScooterAllViewModel> Scooters { get; set; }
}
