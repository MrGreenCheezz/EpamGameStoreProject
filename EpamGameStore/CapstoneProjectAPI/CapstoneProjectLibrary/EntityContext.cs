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

        public EntityContext()
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.Development.json").Build();         
            optionsBuilder.UseNpgsql(config.GetValue<string>("ConnectionStrings:Value"));
        }    
    }
}
