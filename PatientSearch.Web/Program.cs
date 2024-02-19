using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PatientSearch.Application.Interfaces;
using PatientSearch.Application.Services;
using PatientSearch.Infrastructure;
using PatientSearch.Web.Middleware;
using Serilog;
using System.Reflection;
using System.Text;
using System.Text.Json;
try
{
    Log.Information("Starting Patient Search application");

    var builder = WebApplication.CreateBuilder(args);


    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();

    builder.Host.UseSerilog();
    var configuration = builder.Configuration;
    var Secret = configuration.GetValue<string>("JWT:Secret");
    var ValidIssuer = configuration.GetValue<string>("JWT:ValidIssuer");
    var ValidAudience = configuration.GetValue<string>("JWT:ValidAudience");

    builder.Services.AddInfrastructure(configuration);

    builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.UseMemberCasing();
    }).AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
    //builder.Services.AddFluentValidationAutoValidation();

    builder.Services.AddScoped<IPatientService, PatientService>();
   // builder.Services.AddTransient<ErrorHandleMiddleware>();
    //builder.Services.AddTransient<HstsMiddleware>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Patient Search API V1",
            Version = "V1",
            Description = "Patient Search API V1"
        });
        // To Enable authorization using Swagger (JWT)    
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()

                    }
                    });
    });
    builder.Services.AddAuthentication(
    JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = ValidIssuer,
            ValidAudience = ValidAudience,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret))
        };
        opt.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.Response.OnStarting(async () =>
                {
                    // Write to the response in any way you wish
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    await context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        ErrorCode = StatusCodes.Status400BadRequest,
                        Descrption = "You are not authorized! Please provide valide token!"
                    }));
                });

                return Task.CompletedTask;
            }
        };
    });

    // Add services to the container.

    var app = builder.Build();

    
    app.UseMiddleware<ErrorHandleMiddleware>();
    

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Patient Search API V1");
        });
    }
    // app.UseMiddleware<ErrorHandleMiddleware>();
    //app.UseMiddleware<RequestResponseLoggingMiddleware>();
    //app.UseErrorHandleMiddleware();
    //app.UseCors("test");
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.UseMiddleware<RequestResponseLoggingMiddleware>();

    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<PatientSearchDbContext>();
        dataContext.Database.Migrate();
    }

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}