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
            CreateMap<DocumentDto, Document>()
                .ForMember(x => x.IdUser, opt => opt.Ignore());


            CreateMap<DocumentDto, Document>();
            CreateMap<DocumentDto, Document>().ForMember(x => x.IdUser, opt => opt.Ignore());
        }
    }
}
