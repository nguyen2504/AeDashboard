using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Uow;
using Abp.Extensions;
using AeDashboard.Authorization.Users;
using AeDashboard.Calendar;
using AeDashboard.Calendar.Dto;
using AeDashboard.Controllers;
using AeDashboard.Web.Models.Calendar;
using AeDashboard.Web.Models.Loads;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AeDashboard.Web.Controllers
{
    [AbpMvcAuthorize]
    public class CalendarController : AeDashboardControllerBase
    {
        private readonly ICalendarViewService _calendarViewService;
        private readonly UserManager _userManager;

        public CalendarController(ICalendarViewService calendarViewService, UserManager userManager)
        {
            _calendarViewService = calendarViewService;
            _userManager = userManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            //var roles = _userManager.GetRolesAsync(user);
            //if(_calendarViewService.GetAll().Count<5)
            //{
            //    Random r = new Random(12);
            //    for (int i = 1; i <= 1000; i++)
            //    {
            //        for (int j = 0; j < r.Next(0,23); j++)
            //        {
            //            var t = DateTime.Today.AddDays(-i).AddHours(j);
            //            var item = new CalendarViewDto()
            //            {
            //                Admin = "admin " + i,
            //                BeginDate = t,
            //                Day = 0,
            //                EndDate = t,
            //                Users = "user " + i,
            //                Work = "work " + i,
            //                Place = "Quan " + i,
            //                Time = DateTime.Now,
            //                Weekdays = t.DayOfWeek.ToString(),
            //                CreateDate = t,
            //                IsAcive = true
            //            };
            //            _calendarViewService.Create(item);
            //        }
            //    }
            //}
          
        
            return View();
        }

        public IActionResult GetUsers(string ids)
        {
            var users = _userManager.Users.Include(u => u.Roles).ToList();
            return Json(users);
        }
        public JsonResult GetRole()
        {
            var user = _userManager.Users.FirstOrDefault(j => j.Id.Equals(_userManager.AbpSession.UserId));
            if (user!=null&& user.Roles != null)
            {
                return Json(1);
            }
            else
            {
                return Json(0);
            }
        }
        public IActionResult Create()
        {
            var m = new CalendarViewDto() {BeginDate = DateTime.Now};
            return View(m);
        }
        [HttpPost]
        [UnitOfWork]
        public IActionResult Create(CalendarViewDto model)
        {
            //if(model.BeginDate== new DateTime())
            //model.BeginDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                var iduser = _userManager.AbpSession.UserId;
              var entity = model.MapTo<CalendarViewDto>();
                entity.IsAcive = true;
                entity.CreateDate = DateTime.Now;
                entity.Author = _userManager.Users.FirstOrDefault(j=>j.Id.Equals(iduser)).FullName;
                entity.Weekdays = DateTime.Now.DayOfWeek.ToString();
                _calendarViewService.Create(entity);
                ViewBag.mes = "Thanh cong";
              
              
            }
            //Alerts.Success("Success alert message!", "Test Alert");
            return View("Index");

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
            var skip = t.Skip;
            var take = t.Take;
        var tw =    DateTime.Today.DayOfWeek.ToString();
             //var ch = _calendarViewService.LoadsGroupByWeeks(t.Skip, t.Take);
             var dates = _calendarViewService.GetGroupByDates(skip, take);
            //var dates1 = _calendarViewService.GetGroupByDates(skip, take,DateTime.Today);
            //var weeks = _calendarViewService.GetGroupByDates(skip, take, 44);
            //var weeks1 = _calendarViewService.GetGroupByDates(skip, take, 43);

            //var kt = t.Take.Equals(0) ? _calendarViewService.GetDays() : _calendarViewService.GetLoad(t.Skip, t.Take);
         
            return Json(dates);
        }
        public IActionResult SearchWeek(Loads t)
        {
            var skip = t.Skip;
            var take = t.Take;
            var week = int.Parse(t.Week.Split("-")[1]);
            var year= int.Parse(t.Week.Split("-")[0]);
            var weeks = _calendarViewService.GetGroupByDates(skip, take, week,year);
            return Json(weeks);
        }

        public IActionResult SearchDate(Loads t)
        {
            var skip = t.Skip;
            var take = t.Take;
            var date = t.Date;
            var dates = _calendarViewService.GetGroupByDates(skip, take,date);
            return Json(dates);
        }
        public IActionResult SearchName(Loads t)
        {
            var count = t.Count;
          var name = t.Search;
            var dates = _calendarViewService.SearchGroupByDates(count, name);
            return Json(dates);
        }
        public IActionResult Edit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(CalendarView entity)
        {
            //entity.EndDate = DateTime.Now;
            _calendarViewService.Update(entity);
            ViewBag.mes = "Cập Nhật Thành Công";
            return RedirectToAction("Index","Calendar");
        }

       
        //[HttpGet]
        public IActionResult Delete(int id)
        {
            _calendarViewService.Delete(id);
            return RedirectToAction("Index", "Calendar");
        }
    
        public async Task<ActionResult> EditCalendarViewModal(long id)
        {
            var model = await _calendarViewService.GetCalendarView(id);
          //  model.MapTo<CalendarViewModel>()
            return View("Edit", model);

        }
        public async Task<ActionResult> EditCalendarViewModal1(long id)
        {
            var model = await _calendarViewService.GetCalendarView(id);
            //  model.MapTo<CalendarViewModel>()
            return Json( model);

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
