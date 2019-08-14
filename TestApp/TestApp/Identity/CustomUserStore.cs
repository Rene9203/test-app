using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Core.Entities;
using TestApp.Core.Entities.Repositories;

namespace TestApp.Identity
{
    public class CustomUserStore : IUserStore<AppUser>,
        IUserPasswordStore<AppUser>,
        IQueryableUserStore<AppUser>,
        IUserRoleStore<AppUser>
    {
        private readonly IEmployerRepository _employerRepository;
        private readonly IRoleRepository _roleRepository;

        public CustomUserStore(IEmployerRepository employerRepository, IRoleRepository roleRepository)
        {
            _employerRepository = employerRepository;
            _roleRepository = roleRepository;
        }

        private AppUser GetApplicationUser(User user)
        {
            if (user == null)
                return null;

            var appUser = new AppUser() {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash
            };

            return appUser;
        }

        public IQueryable<AppUser> Users => _employerRepository.GetAll().Select(GetApplicationUser).AsQueryable();

        public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            var employer = new Employer(user.FirstName, user.LastName, user.UserName)
                .SetPassword(user.PasswordHash);
            user.Id = employer.Id;

            _employerRepository.Add(employer);

            if (await _employerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken))
                return IdentityResult.Success;
            
            return IdentityResult.Failed(new IdentityError() { Description = "Fail...." });
        }

        public Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _employerRepository.UnitOfWork.Dispose();
        }

        public async Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _employerRepository.GetByIdAsync(userId);
            return GetApplicationUser(user);
        }

        public async Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            Expression<Func<Employer, bool>> exp = e => e.UserName.Contains(normalizedUserName);
            var user = await _employerRepository.GetAllAsync(exp);
            return GetApplicationUser(user.FirstOrDefault());
        }

        public Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Id);
        }

        public Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.UserName);
        }

        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(normalizedName)) throw new ArgumentNullException(nameof(normalizedName));

            user.NormalizedUserName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(passwordHash)) throw new ArgumentNullException(nameof(passwordHash));

            user.PasswordHash = passwordHash;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));

            user.UserName = userName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task AddToRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentException(nameof(user));
            if (string.IsNullOrEmpty(roleName))
                throw new ArgumentException(nameof(roleName));

            var userInDomain = await _employerRepository.GetByIdAsync(user.Id);
            if (userInDomain == null)
                throw new NullReferenceException(user.Id);

            Expression<Func<Role, bool>> exp = e => e.Name == roleName;
            var roles = await _roleRepository.GetAllAsync(exp);
            var role = roles.FirstOrDefault();
            if (role == null)
                throw new NullReferenceException(roleName);

            userInDomain.AddRole(role.Id);
            _employerRepository.Update(userInDomain);
            var success = await _employerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            if (!success)
                throw new Exception($"A error occur when try add this role {roleName} to user {user.FullName}");
        }

        public Task RemoveFromRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken cancellationToken)
        {
            return await _roleRepository.GetRolesByUser(user.Id);
        }

        public async Task<bool> IsInRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            return await _roleRepository.IsInRoleAsync(user.Id, roleName);
        }

        public Task<IList<AppUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
