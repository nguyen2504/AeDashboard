using System;
using System.Collections.Generic;
using System.Text;

namespace AeDashboard.Calendar.Dto
{
   public class GroupByDate
    {
        public  string Date { get; set; }
        public string Weekdays { get; set; }
        public List<CalendarView> CalendarViews { get; set; }
    }
}
