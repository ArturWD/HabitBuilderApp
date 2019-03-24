using HabitBuilder.Core.Models;
using HabitBuilder.Core.ViewModels;
using HabitBuilder.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Services
{
    public class DayManager
    {
        DataContext db { get; set; }
        public DayManager(DataContext db)
        {
            this.db = db;
        }

        public List<HabitCardViewModel> GetHabits(UserProfile user)
        {
            var habits = new List<HabitCardViewModel>();
            var dbHabits = user.Habits;
            foreach (var habit in dbHabits)
            {
                var habitView = new HabitCardViewModel();
                habitView.HabitId = habit.HabitId;
                habitView.HabitName = habit.HabitName;
                habitView.ChainLength = CountChainLength(habit);
                habitView.Progress =  CountChainLength(habit)/21;
                habitView.Week = GetLastWeek(habit);
                if(habit.Category.CategoryName == "")
                {
                    habitView.Category = "Без категории";
                }
                else
                {
                    habitView.Category = habit.Category.CategoryName;
                }

                habits.Add(habitView);
            }

            return habits;
        }

        public void SetStatusesAll(UserProfile user)
        {
            var habits = user.Habits;

            foreach (var habit in habits)
            {
                SetStatusesIndividual(habit);
            }

            
        }

        private void SetStatusesIndividual(Habit habit)
        {
            List<DayStatus> statuses = habit.DayStatuses.OrderBy(s => s.StatusDate).ToList();

            if(statuses.Count != 0 && statuses.Last().StatusDate.Date != DateTime.Now.Date)
            {
                int[] schedule = habit.Days.Select(d => d.DayNumber).ToArray();

                DateTime lastRecord = statuses.Last().StatusDate;
                if (statuses.Last().Status.StatusName == "unmarked")
                {
                    if (schedule.Contains((int)lastRecord.DayOfWeek))
                    {
                        db.Habits.First(h => h.HabitId == habit.HabitId).DayStatuses.OrderBy(s => s.StatusDate).Last().Status = db.Statuses.First(s => s.StatusName == "fail");
                    }
                    else
                    {
                        db.Habits.First(h => h.HabitId == habit.HabitId).DayStatuses.OrderBy(s => s.StatusDate).Last().Status = db.Statuses.First(s => s.StatusName == "skip");
                    }
                }



                while (lastRecord.Date != DateTime.Now.Date)
                {
                    
                    lastRecord = lastRecord.AddDays(1);
                    DayStatus ds = new DayStatus();
                    if (schedule.Contains((int)lastRecord.DayOfWeek))
                    {
                        ds.Status = db.Statuses.First(s => s.StatusName == "fail");
                    }
                    else
                    {
                        ds.Status = db.Statuses.First(s => s.StatusName == "skip");
                    }

                    if(lastRecord.Date == DateTime.Now.Date)
                        ds.Status = db.Statuses.First(s => s.StatusName == "unmarked");


                    ds.StatusDate = lastRecord;
                    db.Habits.First(h => h.HabitId == habit.HabitId).DayStatuses.Add(ds);
              
                }
                db.SaveChanges();
            }

        }

        public int CountChainLength(Habit habit)
        {
            List<DayStatus> statuses = habit.DayStatuses.OrderBy(d => d.StatusDate).Reverse().ToList();
            int chainLength = 0;
            int index = 0;
            try
            {
                while ((statuses[index].Status.StatusName == "success" || statuses[index].Status.StatusName == "skip" || statuses[index].Status.StatusName == "unmarked") && index < statuses.Count - 1)
                {
                    if (statuses[index].Status.StatusName == "success") chainLength++;
                    index++;
                }
            }
            catch { chainLength = -1; }
            
            return chainLength;
        }
        
        private List<DayViewModel> GetLastWeek(Habit habit)
        {
            var days = new List<DayViewModel>();
            var lastweek = habit.DayStatuses.Where(d => d.StatusDate.Date > DateTime.Now.Date.AddDays(-7)).OrderByDescending(d => d.StatusDate);

            foreach (var date in lastweek)
            {
                DayViewModel day = new DayViewModel();
                day.DayStatus = date.Status.StatusName;
                day.DayId = date.DayStatusId;
                day.Date = date.StatusDate;
                day.HasNote = date.NoteHeadline == "" && date.Note == "";
                day.WithDay = true;
                days.Add(day);
            }

            if (days.Count < 7)
            {
                int disabled = 7 - days.Count;
                for (int i = 0; i <disabled ; i++)
                {
                    DayViewModel day = new DayViewModel();
                    day.DayStatus = "disabled";
                    day.WithDay = true;
                    days.Add(day);
                }
            }

            days.Reverse();

            return days;


        }

        public void ChangeStatus(string statusName, int dayId)
        {
            try
            {
                DayStatus day = db.DayStatuses.First(d => d.DayStatusId == dayId);
                day.Status = db.Statuses.First(s => s.StatusName == statusName);
                db.SaveChanges();
            }
            catch { }         
        }
        
    }
}
