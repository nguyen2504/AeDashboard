using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Abp.Domain.Services;

namespace AeDashboard.Fn
{
  public  class Fn:DomainService,IFn
    {
        public int GetWeekOrderInYear(DateTime time)
        {
            CultureInfo myCI = CultureInfo.CurrentCulture;
            System.Globalization.Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            return myCal.GetWeekOfYear(time, myCWR, myFirstDOW);
        }
    }
}
