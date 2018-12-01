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
     List<CalendarView> GetAll();
     List<CalendarView> GetDays();
     List<CalendarView> GetLoad(int skip, int take);
     Task<CalendarView> GetCalendarView(long id);
     bool Create(CalendarViewDto entity);
     Task<bool> Update(CalendarView entity);
     void Delete(int id);
     int MinWeekend();
     int MaxWeekend();
        //List<GroupByWeek> LoadsGroupByWeeks(int skip, int take);
     List<GroupByDate> GetGroupByDates(int skip, int take);
     List<GroupByDate> GetGroupByDates(int skip, int take,DateTime date);
     List<GroupByDate> GetGroupByDates(int skip, int take,int week);
     List<GroupByDate> SearchGroupByDates(int skip, int take, string name);
    }
}
