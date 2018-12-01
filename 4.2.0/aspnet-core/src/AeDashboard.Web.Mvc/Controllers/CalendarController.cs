using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AutoMapper;
using AeDashboard.Calendar;
using AeDashboard.Calendar.Dto;
using AeDashboard.Controllers;
using AeDashboard.Web.Models.Calendar;
using AeDashboard.Web.Models.Loads;
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

            if(_calendarViewService.GetAll().Count<5)
            {
                Random r = new Random(12);
                for (int i = 0; i < 100; i++)
                {
                    var t = DateTime.Today.AddDays(-i).AddHours(r.Next(0,23));
                    var item = new CalendarViewDto()
                    {
                        Admin = "admin " + i,
                        BeginDate = t,
                        Day = 0,
                        EndDate = DateTime.Today,
                        Users = "user " + i,
                        Work = "work " + i,
                        Place = "Quan " + i
                    };
                    _calendarViewService.Create(item);
                }
            }
            return View();
        }

        public IActionResult Create()
        {
            var m = new CalendarViewDto() {BeginDate = DateTime.Now};
            return View(m);
        }
        [HttpPost]
        public IActionResult Create(CalendarViewDto model)
        {
            if(model.BeginDate== new DateTime())
            model.BeginDate = DateTime.Now;
            if (ModelState.IsValid)
            {
              var entity = model.MapTo<CalendarViewDto>();

                _calendarViewService.Create(entity);
                ViewBag.mes = "Thanh cong";
                return RedirectToAction("Index", "Calendar");
            }
            else
            {
                return RedirectToAction("Index", "Calendar");
            }
           
        }
        [HttpGet]
        public JsonResult GetAll()
        {
           var list = _calendarViewService.GetAll();
            foreach (var q in list)
            {
                q.Day = (int)(DateTime.Now - q.BeginDate).TotalHours;
            }
            return Json(list);
        }

        [HttpGet]
        public JsonResult GetLoads(Loads t)
        {
            //var ch = _calendarViewService.LoadsGroupByWeeks(t.Skip, t.Take);
            var kt = t.Take.Equals(0) ? _calendarViewService.GetDays() : _calendarViewService.GetLoad(t.Skip, t.Take);
            return Json(kt);
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(CalendarView entity)
        {
            entity.EndDate = DateTime.Now;
            _calendarViewService.Update(entity);
            ViewBag.mes = "Cập Nhật Thành Công";
            return RedirectToAction("Index","Calendar");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _calendarViewService.Delete(id);
            return Json("Index");
        }
    
        public async Task<ActionResult> EditCalendarViewModal(long id)
        {
            var model = await _calendarViewService.GetCalendarView(id);
            return View("Edit", model.MapTo<CalendarViewModel>());

        }
        //public async Task<ActionResult> EditUserModal(long userId)
        //{
        //    var user = await _userAppService.Get(new EntityDto<long>(userId));
        //    var roles = (await _userAppService.GetRoles()).Items;
        //    var model = new EditUserModalViewModel
        //    {
        //        User = user,
        //        Roles = roles
        //    };
        //    return View("_EditUserModal", model);
        //}
    }
}
