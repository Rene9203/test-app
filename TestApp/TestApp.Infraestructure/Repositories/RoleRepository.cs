using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;
using TestApp.Infraestructure.DbConfig;

namespace TestApp.Infraestructure.Repositories
{
    public class RoleRepository : GenericRepository<Role, string>, IRoleRepository
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<string>> GetRolesByUser(string userId)
        {
            var user = await _context.Employers.Include(x => x.Roles)
                                        .ThenInclude(x => x.Role)
                                        .FirstOrDefaultAsync(x => x.Id == userId);

            var response = new List<string>();
            if (user == null)
                return response;

            foreach (var role in user.Roles)
            {
                response.Add(role.Role.Name);
            }

            return response;
        }

        public async Task<bool> IsInRoleAsync(string userId, string roleName) => await _context.Roles
            .AnyAsync(r => r.Name == roleName &&
                           r.Users.Any(ur => ur.UserId == userId));
    }
}
