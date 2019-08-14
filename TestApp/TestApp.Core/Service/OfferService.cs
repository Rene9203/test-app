using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;
using TestApp.Core.SharedKernel.Service;

namespace TestApp.Core.Service
{
    public class OfferService : GenericService<Offer, string>
    {
        public OfferService(IOfferRepository repository) : base(repository)
        {
        }

        public async Task<int> GetOfferActiveCountAsync(string employerId)
        {
            return await (Repository as IOfferRepository).GetOfferActiveCountAsync(employerId);
        }

        public Dictionary<string, int> GetDetailByCategories()
        {
            return (Repository as IOfferRepository).GetDetailByCategories();
        }
    }
}
