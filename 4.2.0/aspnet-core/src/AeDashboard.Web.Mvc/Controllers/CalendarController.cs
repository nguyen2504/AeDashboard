using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using AeDashboard.Calendar;
using AeDashboard.Calendar.Dto;
using AeDashboard.Controllers;
using AeDashboard.Web.Models.Calendar;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AeDashboard.Web.Controllers
{
    public class CalendarController : AeDashboardControllerBase
    {
        private readonly ICalendarViewService _calendarViewService;

        public CalendarController(ICalendarViewService calendarViewService)
        {
            _calendarViewService = calendarViewService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            var m = new CalendarViewModel() {BeginDate = DateTime.Now};
            return View(m);
        }
        [HttpPost]
        public IActionResult Create(CalendarViewModel model)
        {
            if(model.BeginDate== new DateTime())
            model.BeginDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                var entity = model.MapTo<CalendarViewDto>();
                _calendarViewService.Create(entity);
            }
            ViewBag.mes = "Thanh cong";
            return View("Index");
        }
        [HttpGet]
        public JsonResult GetAll()
        {
            var list = _calendarViewService.GetAll();
            return Json(list);
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(CalendarViewModel model, string id)
        {
            return View();
        }
    }
}
