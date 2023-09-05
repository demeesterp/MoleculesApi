using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using molecules.api.Filter;
using molecules.api.ServiceExtensions;
using molecules.infrastructure.data;
using Okta.AspNetCore;
using System.Reflection;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add the environment values to the configuration
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddMoleculesServices();


builder.Services.AddAuthentication(options =>
      {
            options.DefaultAuthenticateScheme = OktaDefaults.ApiAuthenticationScheme;
            options.DefaultChallengeScheme = OktaDefaults.ApiAuthenticationScheme;
            options.DefaultSignInScheme = OktaDefaults.ApiAuthenticationScheme;
      }).AddOktaWebApi(new OktaWebApiOptions()
      {
            OktaDomain = builder.Configuration["Okta:OktaDomain"],
            AuthorizationServerId = builder.Configuration["Okta:AuthorizationServerId"],
            Audience = builder.Configuration["Okta:Audience"]
      });

builder.Services.AddAuthorization();


builder.Services.AddControllers().AddJsonOptions(
    options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"),true);
});

builder.Services.AddMvcCore(option =>
{
    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                            .Build();
    option.Filters.Add(new AuthorizeFilter(policy));
    option.Filters.Add(new MoleculesExceptionFilter());
});

builder.Services.AddDbContext<MoleculesDbContext>(options => 
                                                    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
