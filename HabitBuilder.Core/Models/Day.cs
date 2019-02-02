using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.Models
{
    [Table("Days")]
    public class Day
    {
        public int DayId { get; set; }
        public int DayNumber { get; set; }
        public virtual ICollection<Habit> Habits { get; set; }
    }
}
