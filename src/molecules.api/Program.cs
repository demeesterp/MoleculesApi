using Microsoft.EntityFrameworkCore;
using molecules.api.Filter;
using molecules.api.ServiceExtensions;
using molecules.infrastructure.data;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add the environment values to the configuration
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddMoleculesServices();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"),true);
});

builder.Services.AddMvcCore(option => option.Filters.Add(new MoleculesExceptionFilter()));

builder.Services.AddDbContext<MoleculesDbContext>(options => 
                    options.UseNpgsql(builder.Configuration["ConnectionString"]?.ToString())
                );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
