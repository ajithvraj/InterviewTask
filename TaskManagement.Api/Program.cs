
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Database;
using TaskManagement.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using TaskManagement.Application.Validators;


namespace TaskManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Adding validation service

            builder.Services.AddControllers();

            builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskDtoValidator>();

            builder.Services.AddFluentValidationAutoValidation();

            //This part will authorize the user and admin

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("UserHeader", new OpenApiSecurityScheme
                {
                    Name = "x-user-id",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Description = "Enter User Id"
                });

                c.AddSecurityDefinition("RoleHeader", new OpenApiSecurityScheme
                {
                    Name = "x-user-role",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Description = "Enter User Role (Admin/User)"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "UserHeader"
                }
            },
            new string[] {}
        },
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "RoleHeader"
                }
            },
            new string[] {}
        }
    });
            });

            //DbContextRegistration 

            builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("TaskDb"));

            //Service Registration 

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITaskRepository,TaskRepository>();
            builder.Services.AddScoped<ITaskServices,TaskService>();

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
