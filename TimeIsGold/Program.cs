using Application.Mapping;
using Application.Services;
using AutoMapper;
using Domain.Ports;
using Domain.Ports.Client;
using Domain.Ports.Enterprise;
using Domain.Ports.Plan;
using Domain.Ports.Professional;
using Domain.Ports.Scheduling;
using Domain.Ports.SchedulingType;
using Domain.ValueObjects;
using Infra.Data.Repositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TimeIsGold
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");

            // Configuração do AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new EntityToDTOMapping());
            });

            builder.Services.AddDbContext<TimeIsGoldDbContext>(options =>
            {
                options.UseNpgsql(connection);
                options.UseLoggerFactory(LoggerFactory.Create(loggingBuilder => loggingBuilder.AddConsole()));
            });

            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            //Injeção de dependência dos repositórios e serviços
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();

            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<IPlanRepository, PlanRepository>();

            builder.Services.AddScoped<ISchedulingService, SchedulingService>();
            builder.Services.AddScoped<ISchedulingRepository, SchedulingRepository>();

            builder.Services.AddScoped<ISchedulingTypeService, SchedulingTypeService>();
            builder.Services.AddScoped<ISchedulingTypeRepository, SchedulingTypeRepository>();

            builder.Services.AddScoped<IEnterpriseService, EnterpriseService>();
            builder.Services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();

            builder.Services.AddScoped<IProfessionalService, ProfessionalService>();
            builder.Services.AddScoped<IProfessionalRepository, ProfessionalRepository>();

            //Jwt Configuração para Admin
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiredType", policy => policy.RequireClaim("Type", "2")); // 2 == Admin
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings")["SecretKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
