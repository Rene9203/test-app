using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;
using TestApp.Infraestructure.DbConfig;

namespace TestApp.Infraestructure.Repositories
{
    public class OfferRepository : GenericRepository<Offer, string>, IOfferRepository
    {
        public OfferRepository(AppDbContext context) : base(context)
        {
        }

        public Dictionary<string, int> GetDetailByCategories()
        {

            var result = _context.Offers.Include(x => x.OfferType)
                .Include(x => x.Candidates)
                .Where(x => x.Active == true)
                .GroupBy(x => x.OfferType.Name).Select(y => new {
                  Name = y.Key,
                  Sum =  y.Sum(x => x.Candidates.Count)
                });

            var response = new Dictionary<string, int>();
            foreach (var item in result)
            {
                response.Add(item.Name, item.Sum);
            }

            return response;
        }

        public async Task<int> GetOfferActiveCountAsync(string employerId)
        {
            var offers = await _context.Offers.Where(x => x.EmployerId == employerId && x.Active).ToListAsync();

            return offers.Count;
        }
    }
}
