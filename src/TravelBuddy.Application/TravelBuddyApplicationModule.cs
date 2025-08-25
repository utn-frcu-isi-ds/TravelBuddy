using Microsoft.Extensions.DependencyInjection;
using TravelBuddy.Application.ExternalApis;
using TravelBuddy.Application.Contracts.ExternalApis;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Account;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;

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

        context.Services.AddTransient<ICityDataProvider, GeoDbCityApiService>();
        context.Services.AddTransient<IEventDataProvider, TicketMasterApiService>();
        context.Services.AddTransient<IGeoDbCityApiService, GeoDbCityApiService>();
        context.Services.AddTransient<ITicketMasterApiService, TicketMasterApiService>();
    }
}
