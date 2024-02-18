using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sprout.Exam.Domain.Models;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Duende.IdentityServer.EntityFramework.Options;

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

        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<EmployeeType>()
                .HasData(new EmployeeType[] 
                {
                    new ()
                    {
                        Id = 1,
                        Tax = (decimal)12.0,
                        TypeName = "Regular"
                    }, 
                    new ()
                    {
                        Id = 2,
                        Tax = null,
                        TypeName = "Contractual"
                    }
                });
        }
    }

}