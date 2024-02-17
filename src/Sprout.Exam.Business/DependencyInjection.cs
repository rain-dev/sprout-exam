using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.Business.Features.Employee.Command;
using Sprout.Exam.Business.Features.Employee.Queries;
using Sprout.Exam.Domain.DTOs;
using Sprout.Exam.Domain.DTOs.Employee.Commands;
using Sprout.Exam.Domain.DTOs.Employee.Query;
using System.Reflection;
namespace Sprout.Exam.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetAssembly(typeof(DependencyInjection)));
            return services;
        }
    }
}
