using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
namespace TimeCraft
{
    internal class AppDBContent: DbContext
    {
        public AppDBContent() :
            base("DBTimeCraft")
        {

        }
        //public AppDBContent(DbContextOptions<AppDBContent> options): base(options)
        //{

        //}
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
