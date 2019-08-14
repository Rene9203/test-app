using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;
using TestApp.Infraestructure.DbConfig;

namespace TestApp.Infraestructure.Repositories
{
    public class OfferTypeRepository : GenericRepository<OfferType, string>, IOfferTypeRepository
    {
        public OfferTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
