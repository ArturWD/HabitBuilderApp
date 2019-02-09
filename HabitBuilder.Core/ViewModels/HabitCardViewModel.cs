using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.ViewModels
{
    public class HabitCardViewModel
    {
        public int HabitId { get; set; }
        public string HabitName { get; set; }
        public ICollection<DayViewModel> Week { get; set; }
        public int ChainLength { get; set; }
        public int Progress { get; set; }
    }
}
