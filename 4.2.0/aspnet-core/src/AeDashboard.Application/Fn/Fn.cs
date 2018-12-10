using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Abp.Domain.Services;
using AeDashboard.Authorization.Users;
using Microsoft.AspNetCore.Http;

namespace AeDashboard.Fn
{
  public  class Fn:DomainService,IFn
    {
        private readonly UserManager _userManager;

        public Fn(UserManager userManager)
        {
            _userManager = userManager;
        }
        public int GetWeekOrderInYear(DateTime time)
        {
            CultureInfo myCI = CultureInfo.CurrentCulture;
            System.Globalization.Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            return myCal.GetWeekOfYear(time, myCWR, myFirstDOW);
        }

        public User User()
        {
            var iduser = _userManager.AbpSession.UserId;
            return _userManager.Users.FirstOrDefault(j => j.Id.Equals(iduser));
        }

        public string UploadFile(IList<IFormFile> files)
        {
            var file = files[0];
            if (file == null || file.Length == 0)
                return "file not selected";
            Random r = new Random(999999);
            var filename = User().Id + "_" + User().UserName + "_" + DateTime.Now.ToString("yyyy-MM-dd") + "_" + r.Next() + "_" + file.FileName;
            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/download/",
                filename);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                 file.CopyTo(stream);
            }
            return filename;
        }

        public string ConvertDaysOrHour(DateTime dt)
        {
            var days = (DateTime.Now.Subtract(dt)).Hours;
            if (days<24)
            {
                if (days < 10)
                {
                    return "0" + days + "h";
                }
                else
                {
                    return days + "h";
                }
              
            }
            else
            {
                return (DateTime.Now.Subtract(dt)).Days + "d";
            }
        }

        public bool RoleUser()
        {
            var user = _userManager.Users.FirstOrDefault(j => j.Id.Equals(_userManager.AbpSession.UserId));
            if (user != null && user.Roles.Count.Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
