using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.Models.Extensions
{
    public static class HabitExtensions
    {
        public static ICollection<Reason> GetReasons(this string[] reasonsText)
        {
            List<Reason> reasons = new List<Reason>();
            foreach (var reason in reasonsText)
            {
                Reason reasonObj = new Reason { ReasonText = reason};
                reasons.Add(reasonObj);
            }
            return reasons;
        }
        /*
        public static ICollection<Reason> GetDays(this int[] dayNumbers)
        {
            List<Day> days = new List<Day>();
            foreach (var day in reasonsText)
            {
                Day dayObj = new Day { DayNumber = day };
                days.Add(reasonObj);
            }
            return days;
        }
        */
    }
}
