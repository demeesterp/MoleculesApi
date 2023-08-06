using Serilog.Events;
using Serilog;
using molecules.core.services;
using FluentValidation;
using molecules.core.services.validators;
using molecules.core.services.validators.servicehelpers;
using molecule.infrastructure.data.interfaces.Repositories;
using molecules.infrastructure.data.Repositories;
using molecules.core.Factories;
using molecules.core.factories;
using molecules.core.factories.Reports;

namespace molecules.api.ServiceExtensions
{
    /// <summary>
    /// IServiceCollection extensions for Molecules API
    /// </summary>
    public static class MoleculeApiServiceExtensions
    {
        /// <summary>
        /// Add all services and middleware for Molecules API
        /// </summary>
        /// <param name="services">The application Services Collection</param>
        /// <returns>The modified services collection</returns>
        public static IServiceCollection AddMoleculesServices(this IServiceCollection services)
        {
            services.AddLogging();
            services.AddCoreServices();

            return services;
        }

        internal static void AddCoreServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreateCalcOrderValidator>();

            services.AddScoped<ICalcOrderServiceValidations, CalcOrderServiceValidations>();
            services.AddScoped<ICalcOrderFactory, CalcOrderFactory>();
            services.AddScoped<ICalcOrderService, CalcOrderService>();
            services.AddScoped<ICalcOrderRepository, CalcOrderRepository>();

            services.AddScoped<ICalcOrderItemServiceValidations, CalcOrderItemServiceValidations>();
            services.AddScoped<ICalcOrderItemFactory, CalcOrderItemFactory>();
            services.AddScoped<ICalcOrderItemService, CalcOrderItemService>();
            services.AddScoped<ICalcOrderItemRepository, CalcOrderItemRepository>();

            services.AddScoped<IGmsCalcInputFactory, GmsCalcInputFactory>();
            services.AddScoped<IMoleculeFromGmsFactory, MoleculeFromGmsFactory>();
            services.AddScoped<ICalcDeliveryService, CalcDeliveryService>();

            services.AddScoped<IMoleculeRepository, MoleculeRepository>();
            services.AddScoped<ICalcMoleculeFactory, CalcMoleculeFactory>();
            services.AddScoped<ICalcMoleculeService, CalcMoleculeService>();

            services.AddScoped<IMoleculeReportFactory, MoleculeReportFactory>();
            services.AddScoped<IMoleculeReportService, MoleculeReportService>();

            services.AddScoped<ICalcFileConversionService, CalcFileConversionService>();
        }

        internal static void AddLogging(this IServiceCollection services)
        {
            var logger = new LoggerConfiguration()
                            .MinimumLevel.Debug()
                                .WriteTo.File(path: "C:\\Data\\Logs\\log.txt",
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
