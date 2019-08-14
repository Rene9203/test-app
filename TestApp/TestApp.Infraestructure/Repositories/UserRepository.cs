using TestApp.Core.Entities;
using TestApp.Infraestructure.DbConfig;

namespace TestApp.Infraestructure.Repositories
{
    public class UserRepository : GenericRepository<User, string>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
