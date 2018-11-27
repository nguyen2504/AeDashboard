using System;
using System.Collections.Generic;
using System.Text;

namespace AeDashboard.Calendar.Dto
{
  public  class GroupByWeek
    {
        public  int Week { get; set; }
        public List<CalendarView> CalendarViews { get; set; }
    }
}
