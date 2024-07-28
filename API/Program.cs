using AutoMapper;
using TaskManager.Infrastructure.Models;
//using Microsoft.Extensions.Configuration;
using System.Text;
using TaskManager.Infrastructure.Repository.Interfaces;
using TaskManager.Infrastructure.Repository;
using TaskManager.API.DTO;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TaskManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            // Add services to the container.  
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        public static void ConfigureServices(IServiceCollection services,
                                             ConfigurationManager configuration)
        {
            var configMapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tarefa, Core.Entities.Tarefa>();
                cfg.CreateMap<Usuario, Core.Entities.Usuario>();
                cfg.CreateMap<Core.Entities.Usuario, UserPutGet>();
                cfg.CreateMap<Core.Entities.Tarefa, Tarefa>();
            });
            var mapper = configMapper.CreateMapper();

            services.AddDbContext<SQLServerContext>();
            services.AddSingleton(mapper);
            services.AddTransient<IUserRepository, EFUserRepository>();
            services.AddTransient<ITarefaRepository, EFTarefaRepository>();

            var configAuthentication = configuration.GetSection("Authentication");
            var key = Encoding.ASCII.GetBytes(configAuthentication.GetValue<string>("SecretKey"));
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configAuthentication.GetValue<string>("Audience"),
                    ValidIssuer = configAuthentication.GetValue<string>("Issuer")
                };
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
