using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AeDashboard.Web.Models.Calendar
{
    public class CalendarViewModel
    {
        [Required]
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public string Work { get; set; }
        public string Admin { get; set; }
        [Required]
        public string Users { get; set; }
        public string Place { get; set; }
    }
}
