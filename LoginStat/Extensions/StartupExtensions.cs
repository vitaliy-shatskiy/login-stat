using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using LoginStat.BLL.MappingProfiles;
using LoginStat.BLL.Services;
using LoginStat.BLL.Services.Abstract;
using LoginStat.DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LoginStat.Extensions
{
    public static class StartupExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetAssembly(typeof(UserMappingProfile)));
        }

        public static void AddValidation(this IServiceCollection services)
        {
            // services
            //     .AddControllers()
            //     .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Validator>());
        }

        public static void AddLoginStatDb(this IServiceCollection services, IConfiguration configuration)
        {
            var migrationAssembly = typeof(LoginStatContext).Assembly.GetName().Name;
            services.AddDbContext<LoginStatContext>(options =>
                options.UseSqlServer(configuration["ConnectionStrings:LoginStatDBConnection"],
                    opt => opt.MigrationsAssembly(migrationAssembly)));
        }

        public static void UseLoginStatContext(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<LoginStatContext>();
            context?.Database.Migrate();
        }

        public static void RegisterCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "LoginStat",
                        Version = "v1"
                    });
            });
        }

        public static void UseSwaggerWithUI(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c
                .SwaggerEndpoint("/swagger/v1/swagger.json", "LoginStat v1"));
        }


        public static void ConfigureCustomValidationErrors(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values
                        .SelectMany(x => x.Errors
                            .Select(p => p.ErrorMessage))
                        .ToList();
                    throw new ValidationException(errors.Aggregate((s, s1) => s + "\n" + s1));
                };
            });
        }
    }
}