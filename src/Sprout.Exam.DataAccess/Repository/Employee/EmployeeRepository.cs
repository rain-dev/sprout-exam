using Sprout.Exam.DataAccess.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository.Employee
{
    public class EmployeeRepository : BaseRepository<Domain.Models.Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(SproutExamDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<int> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var exists = await _dbContext.Set<Domain.Models.Employee>().FindAsync(id);
            if (exists is not null)
            {
                exists.IsDeleted = true;
                _dbContext.Update(exists);
                return await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return -1;
        }

    }
}