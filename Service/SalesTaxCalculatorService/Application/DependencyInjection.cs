using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SalesTaxCalculatorService.Application.Common.PipelineBehaviors;
using System.Reflection;
using SalesTaxCalculatorService.Application.Common.Interfaces;

namespace SalesTaxCalculatorService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //TODO: See if this can be removed
            services.AddAutoMapper(typeof(DependencyInjection));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}