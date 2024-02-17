using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.DataAccess.Repository.Employee;

namespace Sprout.Exam.DataAccess
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
