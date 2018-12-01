using System;
using System.Collections.Generic;
using System.Text;

namespace AeDashboard.Calendar.Dto
{
   public class GroupByDate
    {
        public  DateTime Date { get; set; }
        public List<CalendarView> CalendarViews { get; set; }
    }
}
