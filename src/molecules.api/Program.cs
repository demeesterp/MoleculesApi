using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using molecules.api.Filter;
using molecules.api.ServiceExtensions;
using molecules.infrastructure.data;
using Okta.AspNetCore;
using System.Reflection;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// add the environment values to the configuration
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers().AddJsonOptions(
    options => {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddMvcCore(option =>
{
    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                            .Build();
    option.Filters.Add(new AuthorizeFilter(policy));
    option.Filters.Add(new MoleculesExceptionFilter());
});



builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            ClientCredentials = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("https://dev-16571816.okta.com/oauth2/default/v1/token"),
                AuthorizationUrl = new Uri("https://dev-16571816.okta.com/oauth2/default/v1/authorize"),
                Scopes = new Dictionary<string, string>
                {
                    { "molecules", "Access to Molecules API" },
                } 
            }
        },
    });

    /*options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });*/

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "oauth2", //The name of the previously defined security scheme.
                    Type = ReferenceType.SecurityScheme
                }
            },new List<string>()
        }
    });

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

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

// Add services to the container.
builder.Services.AddMoleculesServices();

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

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.OAuthClientId("0oab45jv6kUut1IQS5d7");
    o.OAuthClientSecret("Vp-Dw9MrGjrdDjoReJqhNd4X3VNmwLX10wkrQEiNAawYwAmXOhvrUa1H7fED9ooc");
    o.OAuthUsePkce();
});

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
