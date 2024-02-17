using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sprout.Exam.Domain.Models;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Sprout.Exam.DataAccess.Persistence
{
    [ExcludeFromCodeCoverage]
    public class SproutExamDbContext : ApiAuthorizationDbContext<Sprout.Exam.Domain.Models.ApplicationUser>
    {
        private TransactionScope _transactionScope;

        public SproutExamDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {

        }

        public virtual DbSet<Employee> Employees { get; set; }

        public TransactionScope BeginTransaction(IsolationLevel isolationLevel)
        {
            if (_transactionScope is not null)
            {
                _transactionScope.Begin();
            }
            else
            {
                _transactionScope = new TransactionScope(Database.BeginTransaction(isolationLevel));
            }

            return _transactionScope;
        }
    }

}