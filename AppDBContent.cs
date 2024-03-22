using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace TimeCraft
{
    internal class AppDBContent : DbContext
    {
        public AppDBContent()
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

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Participant> Participants { get; set; }

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
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TimeCraft;Username=postgres;Password=123");
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=TimeCraft;Username=postgres;Password=Faza2005");
        }
    }
}