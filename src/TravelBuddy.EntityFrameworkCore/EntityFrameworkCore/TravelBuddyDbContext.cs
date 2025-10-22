using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using TravelBuddy; // Para IUserOwned
using TravelBuddy.Destinations;
using TravelBuddy.Ratings;
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
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Users;

namespace TravelBuddy.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ConnectionStringName("Default")]
public class TravelBuddyDbContext :
    AbpDbContext<TravelBuddyDbContext>,
    IIdentityDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Destination> Destinations { get; set; }

    #region Entities from the modules

    // Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    #endregion

    private readonly ICurrentUser _currentUser;

    public TravelBuddyDbContext(DbContextOptions<TravelBuddyDbContext> options, ICurrentUser currentUser)
        : base(options)
    {
        _currentUser = currentUser;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureBlobStoring();

        builder.Entity<Destination>(b =>
        {
            b.ToTable(TravelBuddyConsts.DbTablePrefix + "Destinations", TravelBuddyConsts.DbSchema);
            b.ConfigureByConvention();
        });

        // Filtro global para todas las entidades que implementen IUserOwned
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(IUserOwned).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var userIdProperty = Expression.Property(parameter, nameof(IUserOwned.UserId));
                var currentUserId = Expression.Constant(_currentUser.Id ?? Guid.Empty);
                var body = Expression.Equal(userIdProperty, currentUserId);
                var lambda = Expression.Lambda(body, parameter);
                entityType.SetQueryFilter(lambda);
            }
        }
    }
}
