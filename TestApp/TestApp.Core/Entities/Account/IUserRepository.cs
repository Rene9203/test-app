using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Core.Repositories;

namespace TestApp.Core.Entities
{
    public interface IUserRepository : IRepository<User, string>
    {
    }
}
