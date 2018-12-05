using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Abp.Domain.Services;
using AeDashboard.Authorization.Users;

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
    }
}
