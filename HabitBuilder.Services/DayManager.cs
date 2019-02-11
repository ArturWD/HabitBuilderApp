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
        DataContext db = new DataContext();

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

                habits.Add(habitView);
            }

            return habits;
        }

        private int CountChainLength(Habit habit)
        {
            return 15;
        }
        
        private List<DayViewModel> GetLastWeek(Habit habit)
        {
            var days = new List<DayViewModel>();
            var lastweek = habit.DayStatuses.Where(d => d.StatusDate >= DateTime.Now.AddDays(-7)).OrderByDescending(d => d.StatusDate);

            foreach (var date in lastweek)
            {
                DayViewModel day = new DayViewModel();
                day.DayStatus = date.Status.StatusName;
                day.DayId = date.DayStatusId;
                day.Date = date.StatusDate;
                day.HasNote = date.NoteHeadline == "" && date.Note == "";

                days.Add(day);
            }

            if (days.Count < 7)
            {
                for (int i = 0; i < 7-days.Count; i++)
                {
                    DayViewModel day = new DayViewModel();
                    day.DayStatus = "disabled";

                    days.Add(day);
                }
            }

            return days;


        }
        
    }
}
