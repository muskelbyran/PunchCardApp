using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PunchCardApp.Entities;

namespace PunchCardApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<UserProfile> UserProfiles { get; set; }
    public DbSet<UserAddress> UserAddresses { get; set; }
    public DbSet<PunchCardEntity> PunchCards { get; set; }
    public DbSet<PunchCardUseEntity> PunchCardUses { get; set; }
    public DbSet<FeatureEntity> Features { get; set; }
    public DbSet<FeatureItemEntity> FeaturesItems { get; set; }
    public DbSet<FeedbackEntity> Feedbacks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Define the relationship between Punchcard and PunchcardUse
        builder.Entity<PunchCardEntity>()
            .HasMany(p => p.PunchCardUses)
            .WithOne(u => u.PunchCard)
            .HasForeignKey(u => u.PunchCardId);

        // Define the relationship between Punchcard and UserProfile
        builder.Entity<PunchCardEntity>()
            .HasOne(p => p.UserProfile)
            .WithMany()
            .HasForeignKey(p => p.UserProfileId);
    }
}