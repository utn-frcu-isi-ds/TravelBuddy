using TravelBuddy.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace TravelBuddy;

[DependsOn(
    typeof(TravelBuddyApplicationModule),
    typeof(TravelBuddyDomainTestModule),
    typeof(TravelBuddyEntityFrameworkCoreModule),
    typeof(AbpTestBaseModule)
)]
public class TravelBuddyApplicationTestModule : AbpModule
{

}
