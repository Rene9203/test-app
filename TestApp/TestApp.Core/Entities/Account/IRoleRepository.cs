using System.Collections.Generic;
using System.Threading.Tasks;
using TestApp.Core.Repositories;

namespace TestApp.Core.Entities.Repositories
{
    public interface IRoleRepository : IRepository<Role, string>
    {
        Task<bool> IsInRoleAsync(string id, string roleName);
        Task<IList<string>> GetRolesByUser(string userId);
    }
}
