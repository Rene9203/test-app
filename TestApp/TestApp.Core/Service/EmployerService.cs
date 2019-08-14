using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;
using TestApp.Core.SharedKernel.Service;

namespace TestApp.Core.Service
{
    public class EmployerService : GenericService<Employer, string>
    {
        public EmployerService(IEmployerRepository repository) : base(repository)
        {
        }
    }
}
