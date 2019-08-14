using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestApp.Core.SharedKernel;

namespace TestApp.Core.Entities
{
    public class Role : Entity<string>
    {
        public static string Administrator => "Administrator";

        public static string Employer => "Employer";

        public string Name { get; private set; }

        public List<UserRole> Users { get; set; }

        public Role(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            Id = Guid.NewGuid().ToString();
            Name = name;
            Users = new List<UserRole>();
        }

        public void AddUser(string userId)
        {
            if(!Users.Any(x => x.UserId == userId))
            {
                Users.Add(new UserRole() {
                    UserId = userId,
                    RoleId = Id
                });
            }
        }
    }
}
