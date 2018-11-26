using DevSlop.Slop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevSlop.Models.Admin
{
    public class AdminIndexViewModel
    {
        public IEnumerable<Schedule> Schedules { get; set; }
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public string SortString { get; set; }

        public bool IsSortAscending { get; set; }
    }
}
