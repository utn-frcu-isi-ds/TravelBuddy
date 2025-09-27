using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy.Application.Tests.Destinations;
using TravelBuddy.Samples;
using Xunit;

namespace TravelBuddy.EntityFrameworkCore.Applications
{    
    [Collection(TravelBuddyTestConsts.CollectionDefinitionName)]
    public class EfCoreDestinationAppServiceTests : DestinationAppService_Tests<TravelBuddyEntityFrameworkCoreTestModule>
    {

    }

}
