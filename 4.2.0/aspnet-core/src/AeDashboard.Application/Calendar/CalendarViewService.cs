﻿using System;
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
            var dt = _repository.GetAllList().OrderByDescending(j =>j.EndDate).ToList();
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
            _repository.Insert(dt);
            return true;
        }

        public async Task<bool> Update(CalendarView entity)
        {
            try
            {
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
    }
}
