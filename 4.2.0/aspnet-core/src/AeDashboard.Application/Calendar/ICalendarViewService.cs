using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Services;
using AeDashboard.Calendar.Dto;
using AeDashboard.Users.Dto;

namespace AeDashboard.Calendar
{
 public   interface ICalendarViewService : IDomainService
 {
     Task<List<CalendarView>> GetAll();
     Task<CalendarView> GetCalendarView(long id);
     bool Create(CalendarViewDto entity);
     Task<string> Update(CalendarViewDto entity);
     void Delete(CalendarViewDto entity);
 }
}
