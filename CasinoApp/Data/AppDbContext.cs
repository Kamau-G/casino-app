using Microsoft.EntityFrameworkCore;
using CasinoApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace CasinoApp.Data
{
    public class AppDbContext: IdentityDbContext
    {
        // constructor just calls the base class constructor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }
        public DbSet<Message> Messages { get; set; }
        public DbSet<CardGameScore> CardGameScores { get; set; }
        public DbSet<DiceGameScore> DiceGameScores { get; set; }
        
        //Adding this to be able to add code review


    }
}
