using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using AeDashboard.Calendar.Dto;
using AeDashboard.GetUser;
using AutoMapper;

namespace AeDashboard.Calendar
{
  public  class CalendarViewService: DomainService, ICalendarViewService
  {
      private readonly IGetUserService _getUserService;
      private readonly IRepository<CalendarView> _repository;
        public CalendarViewService(IGetUserService getUserService, IRepository<CalendarView> repository)
      {
          _getUserService = getUserService;
          _repository = repository;
      }
        public List<CalendarView> GetAll()
        {
            var dt = _repository.GetAllList();
            return dt;
        }

        public Task<CalendarView> GetCalendarView(long id)
        {
            throw new NotImplementedException();
        }

        public bool Create(CalendarViewDto entity)
        {
            
            var dt = entity.MapTo<CalendarView>();
            dt.UserId = _getUserService.GetIdUser();
            _repository.Insert(dt);
            return true;
        }

        public Task<string> Update(CalendarViewDto entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(CalendarViewDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
