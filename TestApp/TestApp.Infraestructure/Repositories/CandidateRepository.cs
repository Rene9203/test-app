using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;
using TestApp.Infraestructure.DbConfig;

namespace TestApp.Infraestructure.Repositories
{
    public class CandidateRepository : GenericRepository<Candidate, string>, ICandidateRepository
    {
        public CandidateRepository(AppDbContext context) : base(context)
        {
        }
    }
}
