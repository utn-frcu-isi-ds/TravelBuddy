using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBuddy.Destinations;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace TravelBuddy
{
    public class TravelBuddyDataSeederContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Destination, Guid> _destinationRepository;

        public TravelBuddyDataSeederContributor(IRepository<Destination, Guid> bookRepository)
        {
            _destinationRepository = bookRepository;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            Console.WriteLine("TravelBuddyDataSeederContributor.SeedAsync ejecutado");

            if (await _destinationRepository.GetCountAsync() <= 0)
            {
                await _destinationRepository.InsertAsync(
                    new Destination
                    {
                        Name = "Buenos Aires",
                        Country = "Argentina",
                    },
                    autoSave: true
                );

                await _destinationRepository.InsertAsync(
                    new Destination
                    {
                        Name = "Rio de Janeiro",
                        Country = "Brazil",
                    },
                    autoSave: true
                );
            }
        }
    }
}