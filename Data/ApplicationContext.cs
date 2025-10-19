using DynamicOrderingDemo.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicOrderingDemo.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Person>()
            .HasData(PersonSeed.Create());

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}