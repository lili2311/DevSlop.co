using DevSlop.Slop.Data;
using DevSlop.Slop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevSlop.Slop.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private DevSlopContext _context;

        public ScheduleRepository(DevSlopContext context)
        {
            _context = context;
        }

        public IEnumerable<Schedule> GetCurrentSchedule()
        {
            return _context.Schedules
                .Where(x => x.When >= DateTime.Now)
                .OrderBy(x => x.When);
        }

        public IEnumerable<Schedule> GetPastSchedule()
        {
            return _context.Schedules
                .Where(x => x.When < DateTime.Now)
                .OrderByDescending(x => x.When);
        }

        public IEnumerable<Schedule> GetAllSchedules()
        {
            return _context.Schedules.ToList();
        }

        public IEnumerable<Schedule> Paging(int pageSize, int page)
        {
            return _context.Schedules
                .OrderBy(x => x.When)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public int GetNumSchedules()
        {
            return _context.Schedules.Count();
        }

        public Schedule GetEventSchedule(int id)
        {
            return _context.Schedules.First(item => item.Id == id);
        }

        public void SaveSchedule(Schedule schedule)
        {
            if (schedule.Id == 0)
            {
                _context.Schedules.Add(schedule);
            }
            else
            {
                _context.Entry(schedule).State = EntityState.Modified;
            }
        }

        public void DeleteSchedule(int id)
        {
            var removeSchedule = _context.Schedules.First(x => x.Id == id);
            _context.Schedules.Remove(removeSchedule);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
