using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestApp.Core.Repositories;

namespace TestApp.Core.Entities.Repositories
{
    public interface IOfferRepository : IRepository<Offer, string>
    {
        Task<int> GetOfferActiveCountAsync(string employerId);

        Dictionary<string, int> GetDetailByCategories();
    }
}
