using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Core.Models
{
    [Table("Categories")]
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Категория")]
        public string CategoryName { get; set; }

    }
}
