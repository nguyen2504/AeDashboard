using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Services;

namespace AeDashboard.Fn
{
  public  interface IFn:IDomainService
    {
        int GetWeekOrderInYear(DateTime time);
    }
}
