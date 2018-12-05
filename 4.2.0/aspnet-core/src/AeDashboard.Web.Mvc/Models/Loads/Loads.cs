using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeDashboard.Web.Models.Loads
{
    public class Loads
    {
        public  int Skip { get; set; }
        public int Take { get; set; }
        public string Week { get; set; }
        public DateTime Date { get; set; }
        public string Search { get; set; }
        public int Count { get; set; }
    }
}
