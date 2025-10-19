using Microsoft.EntityFrameworkCore;
using WpfApp1.Models;


namespace WpfApp1.EntityFrameWork
{
    class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }

        public DbSet<WpfApp1.Models.TacheDao> Tasks { get; set; }
        // DbSet properties for other entities

        // Optional: Override OnModelCreating to configure model
    }    
    
}
