using Application.Mapping;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using Application.Services;
using Infra.Data.Repositories;
using Domain.Ports.Plan;
using Domain.Ports.SchedulingType;

namespace TimeIsGold
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<TimeIsGoldDbContext>(options =>
                options.UseNpgsql(connection));

            // Configuração do AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityToDTOMapping());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            //Injeção de dependência dos repositórios e serviços
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();

            builder.Services.AddScoped<ISchedulingTypeService, SchedulingTypeService>();
            builder.Services.AddScoped<ISchedulingTypeRepository, SchedulingTypeRepository>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
