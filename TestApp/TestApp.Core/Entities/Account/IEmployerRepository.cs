using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Core.Repositories;

namespace TestApp.Core.Entities.Repositories
{
    public interface IEmployerRepository : IRepository<Employer, string>
    {
    }
}
