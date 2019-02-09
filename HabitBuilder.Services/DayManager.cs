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
            }
        }

        private int CountChainLength(Habit habit)
        {
            return 15;
        }
        private List<DayViewModel> GetLastWeek(Habit habit)
        {

        }
    }
}
