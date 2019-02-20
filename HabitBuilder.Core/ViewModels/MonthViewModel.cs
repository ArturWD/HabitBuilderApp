using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.ViewModels
{
    public class MonthViewModel
    {
        public string MonthName { get; set; }
        public List<List<DayViewModel>> Days { get; set; }
    }
}
