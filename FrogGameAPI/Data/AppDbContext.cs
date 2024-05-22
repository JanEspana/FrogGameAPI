using Microsoft.EntityFrameworkCore;
using FrogGameAPI.Models;

namespace FrogGameAPI.Models
{
    public class AppDbContext : DbContext
    {
        //Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //DbSet mapeja la classe Student a la base de dades
        public DbSet<Player> Players { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}