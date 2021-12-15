using Microsoft.EntityFrameworkCore;
using WebApplication_Notes.Entities;

namespace WebApplication_Notes.DataAccess.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NotesDB;Trusted_Connection=true");
                optionsBuilder.UseLazyLoadingProxies();
            }   
        }
    }
}
