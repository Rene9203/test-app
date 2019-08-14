using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;
using TestApp.Infraestructure.DbConfig;

namespace TestApp.Infraestructure.Repositories
{
    public class EmployerRepository : GenericRepository<Employer, string>, IEmployerRepository
    {
        public EmployerRepository(AppDbContext context) : base(context)
        {
        }
    }
}
