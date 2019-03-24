using HabitBuilder.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.DAL
{
    public class DataContext: DbContext
    {
        public DataContext()
            : base("name=DefaultConnection")
        {

        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<DayStatus> DayStatuses { get; set; }
        public virtual DbSet<Habit> Habits { get; set; }
        public virtual DbSet<Reason> Reasons { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
