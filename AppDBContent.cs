using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace TimeCraft
{
    internal class AppDBContent : DbContext
    {
        public AppDBContent()
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Task>().HasKey(u => u.Id);
            modelBuilder.Entity<Event>().HasKey(u => u.Id);
            modelBuilder.Entity<Category>().HasKey(u => u.Id);
            modelBuilder.Entity<Participant>().HasKey(u => u.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=TimeCraft;Username=postgres;Password=1234");
        }
    }
}
