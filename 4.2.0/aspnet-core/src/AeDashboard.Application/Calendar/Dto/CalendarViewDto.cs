using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities;
using AeDashboard.Authorization.Users;

namespace AeDashboard.Calendar.Dto
{
    [AutoMapFrom(typeof(CalendarView))]
    public class CalendarViewDto 
    {
        [Required]
        public DateTime BeginDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        [Required]
        public string Work { get; set; }
        public  string Admin { get; set; }
        public string IdAdmins { get; set; }
        [Required]
        public string Users { get; set; }
        public string IdUsers { get; set; }
        public  string Place { get; set; }
        public  int Day { get; set; }
        public int Weekend { get; set; }
        public bool IsAcive { get; set; }
        public string Author { get; set; }
        public string Weekdays { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time{ get; set; }
        public DateTime CreateDate { get; set; }
    }
}
