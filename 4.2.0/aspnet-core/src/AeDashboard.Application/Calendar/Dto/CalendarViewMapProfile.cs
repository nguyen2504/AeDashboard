using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace AeDashboard.Calendar.Dto
{
  public  class CalendarViewMapProfile : Profile
    {
        public CalendarViewMapProfile()
        {
            CreateMap<CalendarViewDto, CalendarView>();
            CreateMap<CalendarViewDto, CalendarView>()
                .ForMember(x => x.UserId, opt => opt.Ignore());
              

            CreateMap<CalendarViewDto, CalendarView>();
            CreateMap<CalendarViewDto, CalendarView>().ForMember(x => x.UserId, opt => opt.Ignore());
        }
    }
}
