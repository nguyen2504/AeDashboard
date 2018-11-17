﻿using System;
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
        [Required]
        public string Users { get; set; }
        public  string Place { get; set; }
      
    }
}
