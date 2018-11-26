using AeDashboard.Calendar;
using AeDashboard.Calendar.Dto;
using AutoMapper;

namespace AeDashboard.Document.Dto
{
  public  class DocumentMapProfile: Profile
    {
        public DocumentMapProfile()
        {
            CreateMap<DocumentDto, Document>();
            CreateMap<CalendarViewDto, CalendarView>()
                .ForMember(x => x.UserId, opt => opt.Ignore());


            CreateMap<CalendarViewDto, CalendarView>();
            CreateMap<CalendarViewDto, CalendarView>().ForMember(x => x.UserId, opt => opt.Ignore());
        }
    }
}
