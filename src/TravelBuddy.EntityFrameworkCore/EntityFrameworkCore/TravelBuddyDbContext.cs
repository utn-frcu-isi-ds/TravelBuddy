using Microsoft.EntityFrameworkCore;
using TravelBuddy.Domain.Entities;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;

namespace TravelBuddy.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ConnectionStringName("Default")]
public class TravelBuddyDbContext :
    AbpDbContext<TravelBuddyDbContext>,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */


    #region Entities from the modules

    /* Notice: We only implemented IIdentityProDbContext 
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityProDbContext .
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    // TravelBuddy Entities
    public DbSet<Destination> Destinations { get; set; }
    public DbSet<FavoriteList> FavoriteLists { get; set; }
    public DbSet<FavoriteListItem> FavoriteListItems { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Comment> Comments { get; set; }

    #endregion

    public TravelBuddyDbContext(DbContextOptions<TravelBuddyDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureBlobStoring();
        
        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(TravelBuddyConsts.DbTablePrefix + "YourEntities", TravelBuddyConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        builder.Entity<Destination>(b =>
        {
            b.ToTable("Destinations");
            b.ConfigureByConvention();
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            b.Property(x => x.Country).IsRequired().HasMaxLength(128);
            b.Property(x => x.PhotoUrl).HasMaxLength(512);
        });
        builder.Entity<FavoriteList>(b =>
        {
            b.ToTable("FavoriteLists");
            b.ConfigureByConvention();
            b.HasMany(x => x.Items).WithOne().HasForeignKey(x => x.FavoriteListId);
        });
        builder.Entity<FavoriteListItem>(b =>
        {
            b.ToTable("FavoriteListItems");
            b.ConfigureByConvention();
        });
        builder.Entity<Notification>(b =>
        {
            b.ToTable("Notifications");
            b.ConfigureByConvention();
            b.Property(x => x.Title).IsRequired().HasMaxLength(256);
            b.Property(x => x.Message).IsRequired().HasMaxLength(1024);
        });
        builder.Entity<Rating>(b =>
        {
            b.ToTable("Ratings");
            b.ConfigureByConvention();
            b.Property(x => x.Stars).IsRequired();
            b.Property(x => x.Comment).HasMaxLength(1024);
        });
        builder.Entity<Comment>(b =>
        {
            b.ToTable("Comments");
            b.ConfigureByConvention();
            b.Property(x => x.Text).IsRequired().HasMaxLength(1024);
        });
    }
}
