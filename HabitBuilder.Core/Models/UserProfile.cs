using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.Models
{
    [Table("UserProfiles")]
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        public virtual ICollection<Habit> Habits { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
