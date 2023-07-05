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
    }

    public static class Category
    {
        public const int NameMaxLength = 50;

        public const int NameMinLength = 2;
    }

    public static class Product
    {
        public const int NameMaxLength = 100;

        public const int NameMinLength = 4;

        public const int ImageUrlMaxLength = 2048;

        public const int ImageUrlMinLength = 2;

        public const int PriceMinValue = 0;

        public const int QuantityMinValue = 0;

        public const int RatingMinValue = 0;

        public const int RatingMaxValue = 5;
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

        public const int TireSizeMaxValue = 20;

        public const int TireSizeMinValue = 1;

        public const int ChargingTimeMaxValue = 24;

        public const int ChargingTimeMinValue = 1;

        public const int EnginePowerMaxValue = 5000;

        public const int EnginePowerMinValue = 100;
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
}
