namespace ThinkElectric.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasData(SeedReviews());
    }

    private List<Review> SeedReviews()
    {
        List<Review> reviews = new List<Review>();

        var review = new Review()
        {
            Id = Guid.Parse("2C2A92EC-55D2-46E0-841B-9024C1659CB1"),
            Content = "This is really nice scooter. Really good for hanging around :)",
            Rating = 9,
            ProductId = Guid.Parse("253A48E4-63C2-4564-B911-B098F37D8370"),
            UserId = Guid.Parse("A44DAD0E-BD5F-4394-9F8D-AD5A31E5C24B"),
        };

        reviews.Add(review);

        review = new Review()
        {
            Id = Guid.Parse("E56A719F-AD51-4BFA-B24B-EAD6EF17192C"),
            Content = "So good company. I recommend their scooters to everyone!",
            Rating = 9,
            UserId = Guid.Parse("A44DAD0E-BD5F-4394-9F8D-AD5A31E5C24B"),
            CompanyId = Guid.Parse("C0781351-133E-4383-81E7-C95E20FA1273"),
        };

        reviews.Add(review);

        return reviews;
    }
}
