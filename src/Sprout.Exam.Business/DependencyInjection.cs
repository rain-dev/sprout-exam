using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sprout.Exam.Business.Common.Behaviors;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
namespace Sprout.Exam.Business
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(DependencyInjection)));

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(DependencyInjection)));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            });
            return services;
        }
    }
}
