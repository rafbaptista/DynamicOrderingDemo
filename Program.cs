using DynamicOrderingDemo.Data;
using DynamicOrderingDemo.Entities;
using DynamicOrderingDemo.Models;
using DynamicOrderingDemo.Repositories;
using DynamicOrderingDemo.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DynamicOrderingDemo;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApplicationContext>(ctx => ctx.UseInMemoryDatabase("in-memory-database"));
        builder.Services.AddScoped<PersonService>();
        builder.Services.AddScoped<PersonRepository>();

        var provider = builder.Services.BuildServiceProvider();
        using var context = provider.GetService<ApplicationContext>();
        context.Database.EnsureCreated();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapGet("/person", async (
                [FromServices] PersonService personService,
                [FromQuery] string? orderBy,
                [FromQuery] bool? orderAsc,
                [FromQuery] int? page,
                [FromQuery] int? pageSize,
                [FromQuery] string? name,
                [FromQuery] int? id,
                [FromQuery] int? age,
                [FromQuery] bool? isActive
            ) =>
            {
                var request = new PersonRequest(orderBy, orderAsc, page, pageSize, name, id, age, isActive);
                return await personService.GetAllAsync(request);
            })
            .WithName("Get Person")
            .WithOpenApi();

        app.Run();
    }
}