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
            try
            {
                Database.EnsureCreated();
            }
            catch(Npgsql.PostgresException ex)
            {
                if (MessageBox.Show($"Не удалось установить соединение с " +
                    "базой данныз TimeCraft\nПоказать ошибку?", "Error",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                    MessageBox.Show($"{ex}", "Error");
                }
            }
        }
        public DbSet<User> User { get; set; }
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
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=TimeCraft;Username=postgres;Password=Faza2005");
        }

        public Object GetById(int id)
        {
            return null;
        }
        public void DeleteById(int id)
        {

        }
        public void ReplaceById(int id)
        {

        }
        public Tuple<Event> GetEventsByUserId(int userId)
        {
            return null;
        }
        public Tuple<Task> GetTasksByUserId(int userId)
        {
            return null;
        }
        public GetEventsByUserIdWhichHeInvited

        public  void Editing_data()
        {
            using (AppDBContent db = new AppDBContent())
            {
                User user = db.User.FirstOrDefault();
                if (user != null)
                {
                    user.Name = "Артур";
                    user.Age = 18;
                    db.User.Update(user);
                    db.SaveChanges();
                }
               
            }
        }

        public void Deleting_data()
        {
            using (AppDBContent db = new AppDBContent())
            {
                User user = db.User.Find(2);
                if (user != null)
                {
                    //удаляем объект
                    db.User.Remove(user);
                    db.SaveChanges();
                }
              
            }
        }

    }
}
