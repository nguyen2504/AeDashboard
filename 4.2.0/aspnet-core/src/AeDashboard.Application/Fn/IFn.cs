using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Services;
using AeDashboard.Authorization.Users;
using Microsoft.AspNetCore.Http;

namespace AeDashboard.Fn
{
  public  interface IFn:IDomainService
    {
        int GetWeekOrderInYear(DateTime time);
        User User();
        string UploadFile(IList<IFormFile> files);
        string ConvertDaysOrHour(DateTime dt);
        bool RoleUser();
    }
}
