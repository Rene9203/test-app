using System.Collections.Generic;
using System.Linq;
using TestApp.Core.SharedKernel;

namespace TestApp.Core.Entities
{
    public class User : Entity<string>
    {
        public string UserName { get; protected set; }

        public string PasswordHash { get; protected set; }

        public List<UserRole> Roles { get; protected set; }

        public void SetRoles(string[] roles)
        {
            Roles.RemoveAll(x => !roles.Any(y => y == x.RoleId));
            var rolesToApply = roles.Except(Roles.Select(x => x.RoleId));
            foreach (var role in rolesToApply)
            {
                AddRole(role);
            }
        }

        public void AddRole(string roleId)
        {
            if(!Roles.Any(x => x.RoleId == roleId))
            {
                Roles.Add(new UserRole() {
                    UserId = Id,
                    RoleId = roleId
                });
            }
        }
    }
}
