using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository.Employee
{
    public interface IEmployeeRepository : IRepository<Domain.Models.Employee, int>
    {
        public Task<int> DeleteAsync(int id, CancellationToken cancellationToken);
    }

}