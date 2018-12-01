using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using AeDashboard.Calendar.Dto;
using AeDashboard.Fn;
using AeDashboard.GetUser;
using AutoMapper;

namespace AeDashboard.Calendar
{
  public  class CalendarViewService: DomainService, ICalendarViewService
  {
      private readonly IGetUserService _getUserService;
      private readonly IRepository<CalendarView> _repository;
      private readonly IFn _fn;

      
        public CalendarViewService(IGetUserService getUserService, IRepository<CalendarView> repository, IFn fn)
      {
          _getUserService = getUserService;
          _repository = repository;
          _fn = fn;
      }
        public List<CalendarView> GetAll()
        {
            var dt = _repository.GetAllList().OrderByDescending(j =>j.BeginDate).ToList();
            return dt;
        }

      public List<CalendarView> GetDays()
      {
          var dt = _repository.GetAllList().Where(j=>j.BeginDate>= DateTime.Today && j.EndDate<= DateTime.Today).OrderByDescending(j => j.Weekend).ToList();
          return dt;
        }

      public List<CalendarView> GetLoad(int skip, int take)
      {
          var dt = _repository.GetAllList().OrderByDescending(j => j.Weekend).Skip(skip).Take(take).ToList();
          foreach (var q in dt)
          {
              q.Day = (int)(DateTime.Today.Date.Subtract(q.BeginDate.Date)).Days;
          }
          return dt;
      }

      public async Task<CalendarView> GetCalendarView(long id)
        {
            var dt = await _repository.FirstOrDefaultAsync((int) id);
            return dt;
        }

        public bool Create(CalendarViewDto entity)
        {
            
            var dt = entity.MapTo<CalendarView>();
            dt.UserId = _getUserService.GetIdUser();
            dt.Weekend = _fn.GetWeekOrderInYear(entity.BeginDate);
            _repository.Insert(dt);
            return true;
        }

        public async Task<bool> Update(CalendarView entity)
        {
            try
            {
                entity.Weekend = _fn.GetWeekOrderInYear(entity.BeginDate);
                entity.UserId = _getUserService.GetIdUser();
                await _repository.InsertOrUpdateAsync(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void Delete(int id)
        {
          _repository.Delete(id);
        }

      public int MinWeekend()
      {
          try
          {
              var min = _repository.GetAllList().Min(q => q.Weekend);
              return min;
          }
          catch (Exception e)
          {
              return 0;
          }
      }

      public int MaxWeekend()
      {
          try
          {
              var min = _repository.GetAllList().Max(q => q.Weekend);
              return min;
          }
          catch (Exception e)
          {
              return 0;
          }
        }

        public List<GroupByDate> GetGroupByDates(int skip, int take)
        {
            var l =  _repository.GetAllListAsync().Result.OrderByDescending(j => j.Weekend).Skip(skip).Take(take).ToList();
            foreach (var q in l)
            {
                q.Day = (int)(DateTime.Today.Date.Subtract(q.BeginDate.Date)).Days;
            }
            var result = new List<GroupByDate>();
            var dates = l.Select(j => j.BeginDate.Date).Distinct();
            foreach (var q in dates)
            {
                result.Add(new GroupByDate()
                {
                    Date = q.Date.Date,
                    CalendarViews = l.FindAll(j=>j.BeginDate.Date.Equals(q.Date.Date))
                });
            }
          
            return result;
        }

        public List<GroupByDate> GetGroupByDates(int skip, int take, DateTime date)
        {
            var l = _repository.GetAllListAsync().Result.Where(j=>j.BeginDate.Date <= date.Date).OrderByDescending(j => j.Weekend).Skip(skip).Take(take).ToList();
            foreach (var q in l)
            {
                q.Day = (int)(DateTime.Today.Date.Subtract(q.BeginDate.Date)).Days;
            }
            var result = new List<GroupByDate>();
            var dates = l.Select(j => j.BeginDate.Date).Distinct();
            foreach (var q in dates)
            {
                result.Add(new GroupByDate()
                {
                    Date = q.Date.Date,
                    CalendarViews = l.FindAll(j => j.BeginDate.Date.Equals(q.Date.Date))
                });
            }
         
            return result;
        }

        public List<GroupByDate> GetGroupByDates(int skip, int take, int week)
        {
          
            var result = new List<GroupByDate>();
            var l = _repository.GetAllList().Where(j => j.Weekend <= week).OrderByDescending(j=>j.Weekend).Skip(skip).Take(take).ToList();
            var dates = l.Select(j => j.BeginDate.Date).OrderByDescending(j=>j.Date).Distinct();
            foreach (var q in dates)
            {
                result.Add(new GroupByDate()
                {
                    Date = q.Date.Date,
                    CalendarViews = l.FindAll(j => j.BeginDate.Date.Equals(q.Date.Date))
                });
            }
            return result;
        }

      public List<GroupByDate> SearchGroupByDates(int skip, int take, string name)
      {
          var result = new List<GroupByDate>();
          var l = _repository.GetAllList().Where(j => 
          j.Author.StartsWith(name) 
          || j.Admin.StartsWith(name)
          ||j.Work.StartsWith(name)
          || j.Place.StartsWith(name)).OrderByDescending(j => j.Weekend).Skip(skip).Take(take).ToList();
          var dates = l.Select(j => j.BeginDate.Date).OrderByDescending(j => j.Date).Distinct();
          foreach (var q in dates)
          {
              result.Add(new GroupByDate()
              {
                  Date = q.Date.Date,
                  CalendarViews = l.FindAll(j => j.BeginDate.Date.Equals(q.Date.Date))
              });
          }
          return result;
        }

      //public List<GroupByWeek> LoadsGroupByWeeks(int skip, int take)
        //{
        //    var load = GetLoad(skip, take);
        //     var ws = new List<GroupByWeek>();
        //    for (int i = load.Max(j=>j.Weekend); i >= load.Min(j => j.Weekend); i--)
        //    {
        //        var items = _repository.GetAllList().Where(j => j.Weekend.Equals(i)).ToList();
        //          var  obj = new GroupByWeek(){Week = i,CalendarViews =  items};
        //          ws.Add(obj);
        //    }
        //    return ws;
        //}
    }
}
