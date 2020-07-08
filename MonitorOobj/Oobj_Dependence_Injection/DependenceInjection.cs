using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Oobj_Service;
using Oobj_Service.Interfaces;
using Oobj_Infra.Middlewares;
using Oobj_Service.Services;
using System;

namespace Oobj_Dependence_Injection
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, string urlBase)
        {
            AddServices(services);

            AddConfigurations(services, urlBase);

            return services;
        }

        private static void AddConfigurations(IServiceCollection services, string urlBase)
        {
            services.TryAddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationMediatRMiddleware<,>));
            services.AddAutoMapper();
            services.AddAssemblyMediatr();

            services.TryAddSingleton<Configuracoes>(new Configuracoes
            {
                UrlBase = urlBase
            });
        }

        private static void AddServices(IServiceCollection services)
        {
            services.TryAddScoped(typeof(IMemoryService), typeof(MemoryService));
            services.TryAddScoped(typeof(ICnpjService), typeof(CnpjService));
        }

        public static void AddAssemblyMediatr(this IServiceCollection services)
        {
            services.AddMediatR(AppDomain.CurrentDomain.Load("Oobj_Aplication"));
        }

        private static void AddAutoMapper(this IServiceCollection services)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(AppDomain.CurrentDomain.Load("Oobj_Aplication"));
            });

            services.TryAddSingleton(configuration.CreateMapper());
        }
    }
}
