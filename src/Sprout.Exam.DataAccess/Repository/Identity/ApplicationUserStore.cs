using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Persistence;
using Sprout.Exam.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository.Identity
{
    public class ApplicationUserStore : IUserStore<ApplicationUser>
    {
        private readonly SproutExamDbContext _dbContext;

        public ApplicationUserStore(SproutExamDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var exists  = await _dbContext.Set<ApplicationUser>()
                .FirstOrDefaultAsync(f=>  f.Id == user.Id, cancellationToken);

            if (exists == null) 
            { 
                _dbContext.Add(user); 
                await _dbContext.SaveChangesAsync();
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError[] { });

        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var t = await _dbContext.Set<ApplicationUser>().FindAsync(userId);
            return t;
        }

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
