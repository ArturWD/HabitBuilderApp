using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.Models
{
    [Table("DayStatus")]
    public class DayStatus
    {
        public int DayStatusId { get; set; }
        [Required]
        public virtual Status Status { get; set; }
        [MaxLength(100)]
        public string NoteHeadline { get; set; }
        public string Note { get; set; }

    }
}
