using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.DataAccess.Repository.Employee;
using System.Diagnostics.CodeAnalysis;

namespace Sprout.Exam.DataAccess
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
