using Serilog.Events;
using Serilog;

namespace molecules.api.ServiceExtensions
{
    public static class MoleculeApiServiceExtensions
    {

        public static IServiceCollection AddMoleculesServices(this IServiceCollection services)
        {
            services.AddLogging();

            return services;
        }



        internal static IServiceCollection AddLogging(this IServiceCollection services)
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

            return services;
        }


    }
}
