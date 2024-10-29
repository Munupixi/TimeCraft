using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows.Forms;

namespace TimeCraft
{
    internal class DataBaseContent : DbContext
    {
        public DataBaseContent()
        {
            while (true)
            {
                try
                {
                    Database.EnsureCreated();
                    return;
                }
                catch (Exception ex)
                {
                    switch (MessageBox.Show("Не удалось установить соединение с " +
                        $"базой данных TimeCraft\n\n{ex}\n\n" +
                        $"Поведение програмыы может быть непредсказуемым.\n" +
                        $"Продолжать - не советуем.", "Error",
                        MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error))
                    {
                        case DialogResult.Abort:
                            Environment.Exit(1);
                            break;

                        case DialogResult.Ignore:
                            return;
                    }
                }
            }
        }

        public DbSet<User> User { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Participant> Participant { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            modelBuilder.Entity<Task>().HasKey(t => t.TaskId);
            modelBuilder.Entity<Event>().HasKey(e => e.EventId);
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<Participant>().HasKey(p => p.ParticipantId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TimeCraft;Username=postgres;Password=Faza2005");
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TimeCraft;Username=postgres;Password=Faza2005");

            //var config = new ConfigurationBuilder()
            //        .SetBasePath(Directory.GetCurrentDirectory())
            //        .AddJsonFile("appsettings.json")
            //        .Build();
        }
    }
}