using AutoMapper;
using TaskManager.Infrastructure.Models;
using TaskManager;

namespace TaskManager.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TaskManagerContext>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Tarefa, Core.Entidades.Tarefa>();
                    //.ForMember(d => d.Status, f => f.MapFrom)
                cfg.CreateMap<Usuario, Core.Entidades.Usuario>();
            });
            IMapper mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
        }
    }
}
