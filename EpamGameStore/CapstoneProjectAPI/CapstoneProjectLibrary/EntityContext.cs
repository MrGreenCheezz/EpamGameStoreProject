using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProjectLibrary
{
    public class EntityContext : DbContext
    {
        public DbSet<GameItem> Games { get; set; }
        public DbSet<GameGenres> GameGenres { get; set; }
        public DbSet<GameGenre> genresList { get; set; }       
        public DbSet<User> users { get; set; }

        private readonly IDbConfig _configuration = new DbConfiguration();

        public EntityContext()
        {
            Database.EnsureCreated();        
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString(ConfigurationTypes.Development));
        }    
    }
}
