using HabitBuilder.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.ViewModels
{
    public class HabitViewModel
    {
        public int HabitId { get; set; }
        public string HabitName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Reason> Reasons { get; set; }
        public Category Category { get; set; }
        public List<MonthViewModel> Months { get; set; }

        public HabitViewModel(Habit dbHabit)
        {
            HabitId = dbHabit.HabitId;
            HabitName = dbHabit.HabitName;
            Description = dbHabit.Description;
            Reasons = dbHabit.Reasons;
            Category = dbHabit.Category;
            Months = GetMonths(dbHabit.DayStatuses);

        }

        private List<MonthViewModel> GetMonths(ICollection<DayStatus> dbDays)
        {
            var months = new List<MonthViewModel>();
            var dbMonths = dbDays.OrderBy(d => d.StatusDate).GroupBy(d => d.StatusDate.Month);

            foreach(var monthItem in dbMonths)
            {
                var monthDays = monthItem.ToList();

                MonthViewModel month = new MonthViewModel();
                month.MonthName = monthItem.First().StatusDate.ToString("MMMMMMMMMMMMM");
                month.Days = new List<List<DayViewModel>>();
                int i = 0;

                while(i < monthDays.Count())
                {
                    var week = new List<DayViewModel>();
                    week.Add(CreateCalendarDay(ref i, monthDays, 0));
                    week.Add(CreateCalendarDay(ref i, monthDays, 1));
                    week.Add(CreateCalendarDay(ref i, monthDays, 2));
                    week.Add(CreateCalendarDay(ref i, monthDays, 3));
                    week.Add(CreateCalendarDay(ref i, monthDays, 4));
                    week.Add(CreateCalendarDay(ref i, monthDays, 5));
                    week.Add(CreateCalendarDay(ref i, monthDays, 6));
                    month.Days.Add(week);
                }

                months.Add(month);
            }


            return months;

        }

        private DayViewModel CreateCalendarDay(ref int i, List<DayStatus> dbDays, int dayOfWeek)
        {
            DayViewModel day = new DayViewModel();
            if (i < dbDays.Count() && (int)dbDays[i].StatusDate.DayOfWeek == dayOfWeek)
            {
                day.DayStatus = dbDays[i].Status.StatusName;
                day.DayId = dbDays[i].DayStatusId;
                day.Date = dbDays[i].StatusDate;
                day.HasNote = dbDays[i].NoteHeadline == "" && dbDays[i].Note == "";
                i++;
            }
            else
            {            
                day.DayStatus = "disabled";
            }
            

            return day;
        }

    }
}
