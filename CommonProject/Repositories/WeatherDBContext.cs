using CommonProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonProject.Repositories
{
    public class WeatherDBContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<WeatherInfo> WeatherInfos { get; set; }
        public DbSet<City> Cities { get; set; }

        public WeatherDBContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated(); 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString);
        }
    }
}
