namespace ThinkElectric.Data.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(SeedPosts());
    }

    private List<Post> SeedPosts()
    {
        var posts = new List<Post>();

        var post = new Post()
        {
            Id = Guid.Parse("BD96C711-32E7-483B-B1FE-9C19C1E0A936"),
            Title = "Kaabo Wolf Warrior 11 opinions and thoughts",
            Content = "The Wolf Warrior 11 is the most powerful, all terrain electric scooter from Kaabo. It has dual 1200W brushless motors, hydraulic brakes, 11 inch off road tires and a massive 35Ah battery for a maximum range of 150 km. The Wolf Warrior 11 is the ultimate electric scooter for extreme riding.",
            CreatedOn = DateTime.Parse("2021-01-01"),
            UserId = Guid.Parse("553D4978-B893-4670-8808-3C91D6165C82"),
        };

        posts.Add(post);

        post = new Post()
        {
            Id = Guid.Parse("C202CB91-30E9-49E2-8C15-D12EEC51B3F7"),
            Title = "Vsett 8",
            Content = "Vsett 8 is the most powerful, all terrain electric scooter from Vsett. It has dual 1200W brushless motors, hydraulic brakes, 11 inch off road tires and a massive 35Ah battery for a maximum range of 150 km. The vsett 8 is the ultimate electric scooter for extreme riding.",
            CreatedOn = DateTime.Parse("2021-02-01"),
            UserId = Guid.Parse("95F65567-3392-47FD-AE6A-95D16DFDBFE2"),
        };

        posts.Add(post);

        return posts;
    }
}
