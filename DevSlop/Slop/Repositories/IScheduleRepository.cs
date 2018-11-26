using DevSlop.Slop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevSlop.Slop.Repositories
{
    public interface IScheduleRepository
    {
        Schedule GetEventSchedule(int id);

        IEnumerable<Schedule> GetAllSchedules();

        IEnumerable<Schedule> Paging(int pageSize, int page);

        IEnumerable<Schedule> GetCurrentSchedule();

        IEnumerable<Schedule> GetPastSchedule();

        int GetNumSchedules();

        void SaveSchedule(Schedule schedule);

        void DeleteSchedule(int id);

        void SaveChanges();


    }
}
