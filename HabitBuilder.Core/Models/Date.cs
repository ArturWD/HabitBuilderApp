using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.Models
{
    [Table("Dates")]
    public class Date
    {
        public int DateId { get; set; }
        public DateTime DateDay { get; set; }
        public virtual ICollection<DayStatus> DayStatuses { get; set; }

    }
}
