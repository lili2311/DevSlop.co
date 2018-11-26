using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevSlop.Models.Admin;
using DevSlop.Slop.Data.Entities;
using DevSlop.Slop.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevSlop.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private static readonly int PAGESIZE = 7;
        private IScheduleRepository _scheduleRepo;

        public AdminController(IScheduleRepository scheduleRepository)
        {
            _scheduleRepo = scheduleRepository;
        }

        public IActionResult Index(int page = 1)
        {
            var scheduleList = _scheduleRepo.Paging(PAGESIZE, page);
            var totalItems = _scheduleRepo.GetNumSchedules();
            var vm = new AdminIndexViewModel
            {
                Schedules = scheduleList,
                TotalItems = totalItems,
                ItemsPerPage = PAGESIZE,
                CurrentPage = page
            };

            return View("Index", vm);
        }

        public IActionResult AddNewEvent()
        {
            return View("AddNewEvent", new Schedule());
        }

        public IActionResult ViewEvent(int id)
        {
            try
            {
                var eventSchedule = _scheduleRepo.GetEventSchedule(id);
                return View("AddNewEvent", eventSchedule);
            }
            catch(Exception)
            {
                return Index();
            }
        }

        [HttpPost]
        public IActionResult SaveEvent(string button, Schedule schedule)
        {
            if (button.Equals("Save"))
            {
                _scheduleRepo.SaveSchedule(schedule);
                _scheduleRepo.SaveChanges();
            }

            return Index();
        }

        public IActionResult DeleteEvent(int eventId)
        {
            _scheduleRepo.DeleteSchedule(eventId);
            _scheduleRepo.SaveChanges();

            return Index();
        }
    }
}