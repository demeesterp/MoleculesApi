using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using molecules.console;
using molecules.console.App;
using Microsoft.EntityFrameworkCore;
using molecules.infrastructure.data;

var builder = Host.CreateDefaultBuilder(args)
       .ConfigureServices((hostContext, services) => {

           services.AddMoleculesServices();
           
           services.AddDbContext<MoleculesDbContext> (
                        options => options.UseNpgsql(hostContext.Configuration["ConnectionString"]?.ToString()));
           
           services.AddHostedService<MoleculesApp>();
        
       });

builder.ConfigureAppConfiguration(options => {
    options.AddEnvironmentVariables();
});

await builder.Build().StartAsync();

