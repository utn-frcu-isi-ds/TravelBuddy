using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Users;
using NSubstitute;

namespace TravelBuddy.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class TravelBuddyDbContextFactory : IDesignTimeDbContextFactory<TravelBuddyDbContext>
{
    public TravelBuddyDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        TravelBuddyEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<TravelBuddyDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        // NSubstitute para ICurrentUser para migraciones y tooling
        var currentUserSub = Substitute.For<ICurrentUser>();
        currentUserSub.Id.Returns((Guid?)null);
        
        return new TravelBuddyDbContext(builder.Options, currentUserSub);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TravelBuddy.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
