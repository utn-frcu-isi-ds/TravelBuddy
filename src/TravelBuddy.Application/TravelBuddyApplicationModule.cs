using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;
using TravelBuddy.CitySearch;
using TravelBuddy.External.CitySearch;

namespace TravelBuddy;

[DependsOn(
    typeof(TravelBuddyDomainModule),
    typeof(TravelBuddyApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class TravelBuddyApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<TravelBuddyApplicationModule>();
        });

        // Registro de GeoDbCitySearchService como implementación de ICitySearchService
        context.Services.AddTransient<ICitySearchService, GeoDbCitySearchService>();
    }
}
