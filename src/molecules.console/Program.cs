using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using molecules.console;
using molecules.console.App;
using molecules.infrastructure.data;
using System.Reflection;

var builder = Host.CreateDefaultBuilder(args)
       .ConfigureServices((hostContext, services) => {

           services.AddMoleculesServices(hostContext.Configuration["basePath"]?.ToString());

           services.AddDbContext<MoleculesDbContext>(
                        options => options.UseNpgsql(hostContext.Configuration.GetConnectionString("ConnectionString")), 
                                                                ServiceLifetime.Transient, 
                                                                    ServiceLifetime.Transient);
           services.AddHostedService<MoleculesApp>();      
       });

builder.ConfigureAppConfiguration((hostContext, options) => {
    options.AddEnvironmentVariables();
    options.AddCommandLine(args);
    options.AddJsonFile("appsettings.json", optional: false);
    options.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false);
    options.AddUserSecrets(Assembly.GetExecutingAssembly());
});

await builder.Build().RunAsync();

