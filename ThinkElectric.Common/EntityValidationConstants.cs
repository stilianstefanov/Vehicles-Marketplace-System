namespace ThinkElectric.Common;

public static class EntityValidationConstants
{
    public static class Address
    {
        public const int CountryMaxLength = 60;

        public const int CountryMinLength = 4;

        public const int CityMaxLength = 60;

        public const int CityMinLength = 4;

        public const int StreetMaxLength = 100;

        public const int StreetMinLength = 4;

        public const int ZipCodeMinLength = 4;

        public const int ZipCodeMaxLength = 10;
    }

    public static class Company
    {
        public const int NameMaxLength = 100;

        public const int NameMinLength = 4;

        public const int EmailMaxLength = 100;

        public const int PhoneNumberMaxLength = 20;

        public const string PhoneNumberRegex = @"^(\+\d{1,3}[- ]?)?\d{10}$";

        public const int WebSiteMaxLength = 100;

        public const int DescriptionMaxLength = 1000;

        public const int DescriptionMinLength = 10;

        public const int ImageIdMaxLength = 30;
    }

    public static class Product
    {
        public const int NameMaxLength = 100;

        public const int NameMinLength = 4;

        public const int ImageIdMaxLength = 30;

        public const string PriceMinValue = "0";

        public const string PriceMaxValue = "20000";

        public const int QuantityMinValue = 0;

        public const int QuantityMaxValue = 1000;
    }


    public static class Scooter
    {
        public const int BrandMaxLength = 100;

        public const int BrandMinLength = 2;

        public const int ModelMaxLength = 100;

        public const int ModelMinLength = 2;

        public const int ColorMaxLength = 100;

        public const int ColorMinLength = 2;

        public const int BatteryMaxLength = 100;

        public const int BatteryMinLength = 2;

        public const int RangeMaxValue = 500;

        public const int RangeMinValue = 1;

        public const int TopSpeedMaxValue = 200;

        public const int TopSpeedMinValue = 1;

        public const int WeightMaxValue = 200;

        public const int WeightMinValue = 1;

        public const int MaxLoadMaxValue = 200;

        public const int MaxLoadMinValue = 1;

        public const int TireSizeMaxValue = 20;

        public const int TireSizeMinValue = 1;

        public const int ChargingTimeMaxValue = 24;

        public const int ChargingTimeMinValue = 1;

        public const int EnginePowerMaxValue = 5000;

        public const int EnginePowerMinValue = 100;

        public const int ScooterTypeMaxValue = 6;

        public const int ScooterTypeMinValue = 1;

        public const int EngineTypeMaxValue = 2;

        public const int EngineTypeMinValue = 1;

        public const int BrakesTypeMaxValue = 3;

        public const int BrakesTypeMinValue = 1;
    }

    public static class Bike
    {
        public const int BrandMaxLength = 100;

        public const int BrandMinLength = 2;

        public const int ModelMaxLength = 100;

        public const int ModelMinLength = 2;

        public const int ColorMaxLength = 100;

        public const int ColorMinLength = 2;

        public const int BatteryMaxLength = 100;

        public const int BatteryMinLength = 2;

        public const int FrameMaterialMaxLength = 100;

        public const int FrameMaterialMinLength = 2;

        public const int RangeMaxValue = 500;

        public const int RangeMinValue = 1;

        public const int TopSpeedMaxValue = 200;

        public const int TopSpeedMinValue = 1;

        public const int WeightMaxValue = 200;

        public const int WeightMinValue = 1;

        public const int MaxLoadMaxValue = 200;

        public const int MaxLoadMinValue = 1;

        public const int WheelSizeMaxValue = 20;

        public const int WheelSizeMinValue = 1;

        public const int ChargingTimeMaxValue = 24;

        public const int ChargingTimeMinValue = 1;

        public const int EnginePowerMaxValue = 5000;

        public const int EnginePowerMinValue = 100;

        public const int FrameSizeMinValue = 10;

        public const int FrameSizeMaxValue = 200;

        public const int GearsCountMinValue = 1;

        public const int GearsCountMaxValue = 50;

        public const int BikeTypeMaxValue = 5;

        public const int BikeTypeMinValue = 1;

        public const int FrameTypeMaxValue = 2;

        public const int FrameTypeMinValue = 1;

        public const int SuspensionTypeMaxValue = 4;

        public const int SuspensionTypeMinValue = 1;

        public const int BrakesTypeMaxValue = 3;

        public const int BrakesTypeMinValue = 1;

        public const int EngineTypeMaxValue = 2;

        public const int EngineTypeMinValue = 1;
    }

    public static class Accessory
    {
        public const int BrandMaxLength = 100;

        public const int BrandMinLength = 2;

        public const int CompatibleModelMaxLength = 100;

        public const int CompatibleModelMinLength = 2;

        public const int CompatibleBrandMaxLength = 100;

        public const int CompatibleBrandMinLength = 2;

        public const int DescriptionMaxLength = 1000;

        public const int DescriptionMinLength = 10;
    }

    public static class Review
    {
        public const int ContentMaxLength = 1000;

        public const int ContentMinLength = 10;
    }

    public static class User
    {
        public const int FirstNameMaxLength = 100;

        public const int FirstNameMinLength = 2;

        public const int LastNameMaxLength = 100;

        public const int LastNameMinLength = 2;

        public const int PhoneNumberMaxLength = 20;

        public const string PhoneNumberRegex = @"^(\+\d{1,3}[- ]?)?\d{10}$";

        public const int PasswordMaxLength = 100;

        public const int PasswordMinLength = 3;
    }

    public static class CompanyQuery
    {
        public const int SearchTermMaxLength = 100;

        public const int SortingMaxValue = 3;

        public const int SortingMinValue = 0;
    }

    public static class ProductQuery
    {
        public const int SearchTermMaxLength = 100;

        public const int SortingMaxValue = 5;

        public const int SortingMinValue = 0;

        public const int ProductTypeMaxValue = 3;

        public const int ProductTypeMinValue = 0;
    }

    public static class ScooterQuery
    {
        public const int SearchTermMaxLength = 100;

        public const int SortingMaxValue = 11;

        public const int SortingMinValue = 0;

        public const int ScooterTypeMaxValue = 6;

        public const int ScooterTypeMinValue = 0;

        public const int EngineTypeMaxValue = 2;

        public const int EngineTypeMinValue = 0;

        public const int BrakesTypeMaxValue = 3;

        public const int BrakesTypeMinValue = 0;
    }
}
