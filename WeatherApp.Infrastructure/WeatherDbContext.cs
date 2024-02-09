using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WeatherApp.Domain;

namespace WeatherApp.Infrastructure;

public class WeatherDbContext:DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options)
        :base(options)
    {
        
    }

    public virtual DbSet<WeatherHistory> WeatherHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}