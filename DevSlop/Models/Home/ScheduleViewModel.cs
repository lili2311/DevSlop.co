using DevSlop.Slop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevSlop.Models.Home
{
    public class ScheduleViewModel
    {
        public IEnumerable<Schedule> CurrentScheduleList { get; set; }
        public IEnumerable<Schedule> PastScheduleList { get; set; }
    }
}
