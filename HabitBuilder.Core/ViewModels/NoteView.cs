using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.ViewModels
{
    public class NoteView
    {
        public int DayId { get; set; }
        public string date;

        public DateTime Date
        {
            set { date = value.ToString("dd.MM.yyyy"); }
        }

        public string Headline { get; set; }
        public string NoteText { get; set; }
    }
}
