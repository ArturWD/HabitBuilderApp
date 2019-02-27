using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.ViewModels
{
    public class DayViewModel
    {
        public int DayId { get; set; }
        public string DayStatus { get; set; }
        public bool HasNote { get; set; }
        public bool WithDay { get; set; }
        public DateTime Date { get; set; }
        public DayViewModel()
        {
            WithDay = false;
        }
    }
}
