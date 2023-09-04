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
using molecules.core.factories.Reports;
using molecules.console.App;

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
            services.AddApps();
            return services;
        }

        internal static void AddApps(this IServiceCollection services)
        {
            services.AddSingleton<CalcDeliveryApp>();
            services.AddSingleton<CalcConversionApp>();
            services.AddSingleton<MoleculeReportApp>();
        }

        internal static void AddCoreServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateCalcOrderValidator>(ServiceLifetime.Transient);

            services.AddTransient<ICalcOrderServiceValidations, CalcOrderServiceValidations>();
            services.AddTransient<ICalcOrderFactory, CalcOrderFactory>();
            services.AddTransient<ICalcOrderService, CalcOrderService>();
            services.AddTransient<ICalcOrderRepository, CalcOrderRepository>();

            services.AddTransient<ICalcOrderItemServiceValidations, CalcOrderItemServiceValidations>();
            services.AddTransient<ICalcOrderItemFactory, CalcOrderItemFactory>();            
            services.AddTransient<ICalcOrderItemService, CalcOrderItemService>();
            services.AddTransient<ICalcOrderItemRepository, CalcOrderItemRepository>();

            services.AddTransient<IGmsCalcInputFactory, GmsCalcInputFactory>();
            services.AddTransient<IMoleculeFromGmsFactory, MoleculeFromGmsFactory>();
            services.AddTransient<ICalcDeliveryService, CalcDeliveryService>();

            services.AddTransient<IMoleculeRepository, MoleculeRepository>();
            services.AddTransient<ICalcMoleculeFactory, CalcMoleculeFactory>();
            services.AddTransient<ICalcMoleculeService, CalcMoleculeService>();

            services.AddTransient<IMoleculeReportFactory, MoleculeReportFactory>();
            services.AddTransient<IMoleculeReportService, MoleculeReportService>();

            services.AddTransient<IMoleculeFileService, MoleculeFileService>();

            services.AddTransient<ICalcFileConversionService, CalcFileConversionService>();

        }

        internal static void AddLogging(this IServiceCollection services, string basePath)
        {
            var logger = new LoggerConfiguration()
                                .MinimumLevel.Debug()
                                .WriteTo.File(path: Path.Combine(basePath, "Logs", "log.txt"),
                                                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                                                rollingInterval: RollingInterval.Day,
                                                restrictedToMinimumLevel: LogEventLevel.Information)
                                .CreateLogger();


            services.AddLogging(loggingBuilder => {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(logger, dispose: true);
            });
        }
    }
}
