using Serilog.Events;
using Serilog;
using molecules.core.services;
using FluentValidation;
using molecules.core.services.validators;
using molecules.core.services.validators.servicehelpers;
using molecule.infrastructure.data.interfaces.Repositories;
using molecules.infrastructure.data.Repositories;
using molecules.core.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using molecules.core.factories;

namespace molecules.console
{
    public static class MoleculeConsoleServiceExtensions
    {
        /// <summary>
        /// Add all services and middleware for Molecules API
        /// </summary>
        /// <param name="services">The application Services Collection</param>
        /// <returns>The modified services collection</returns>
        public static IServiceCollection AddMoleculesServices(this IServiceCollection services, string? basePath)
        {
            services.AddLogging(basePath??Directory.GetCurrentDirectory());
            services.AddCoreServices();

            return services;
        }

        internal static void AddCoreServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateCalcOrderValidator>();

            services.AddSingleton<ICalcOrderServiceValidations, CalcOrderServiceValidations>();
            services.AddSingleton<ICalcOrderFactory, CalcOrderFactory>();
            services.AddSingleton<ICalcOrderService, CalcOrderService>();
            services.AddSingleton<ICalcOrderRepository, CalcOrderRepository>();

            services.AddSingleton<ICalcOrderItemServiceValidations, CalcOrderItemServiceValidations>();
            services.AddSingleton<ICalcOrderItemFactory, CalcOrderItemFactory>();            
            services.AddSingleton<ICalcOrderItemService, CalcOrderItemService>();
            services.AddSingleton<ICalcOrderItemRepository, CalcOrderItemRepository>();

            services.AddSingleton<ICalcDeliveryFactory, CalcDeliveryFactory>();
            services.AddSingleton<ICalcDeliveryService, CalcDeliveryService>();

        }

        internal static void AddLogging(this IServiceCollection services, string basePath)
        {
            var logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                                .WriteTo.File(path: Path.Combine(basePath, "Logs", "log.txt"),
                                                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                                                rollingInterval: RollingInterval.Day,
                                                restrictedToMinimumLevel: LogEventLevel.Information)
                                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
                                .CreateLogger();


            services.AddLogging(loggingBuilder => {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(logger, dispose: true);
            });
        }
    }
}
