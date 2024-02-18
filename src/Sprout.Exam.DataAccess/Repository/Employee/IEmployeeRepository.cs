using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository.Employee
{
    public interface IEmployeeRepository : IRepository<Domain.Models.Employee, int>
    {
        /// <summary>
        /// Deletes an entity asynchronously.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        /// <param name="cancellationToken">Cancellation token (optional).</param>
        public Task<int> DeleteAsync(int id, CancellationToken cancellationToken);
    }

}