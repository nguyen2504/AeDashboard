using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;

namespace AeDashboard.Calendar
{
   public class CalendarView:Entity<int>
    {
        [Required]
        public DateTime BeginDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required]
        public string Work { get; set; }
        public string Admin { get; set; }
        [Required]
        public string Users { get; set; }
        public string Place { get; set; }
        [Required]
        public long UserId { get; set; }
    }
}
