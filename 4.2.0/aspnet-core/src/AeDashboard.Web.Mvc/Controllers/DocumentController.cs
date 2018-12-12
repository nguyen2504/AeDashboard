using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Zero.Configuration;
using AeDashboard.Authorization.Roles;
using AeDashboard.Authorization.Users;
using AeDashboard.Controllers;
using AeDashboard.Document;
using AeDashboard.Document.Dto;
using AeDashboard.Fn;
using AeDashboard.Roles;
using AeDashboard.Web.Models.Loads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AeDashboard.Web.Controllers
{
    [AbpMvcAuthorize]
    public class DocumentController : AeDashboardControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly UserManager _userManager;
        private readonly IFn _fn;
     
        public DocumentController(IDocumentService documentService, UserManager userManager,IFn fn
          
            )
        {
            _documentService = documentService;
            _userManager = userManager;
            _fn = fn;
        
        }
        public IActionResult Index()
        {
           

            //var check = _role.GetAllPermissions().Result.Items.;
            //if (_documentService.GetAll().Count < 3)
            //{
            //    for (int i = 0; i < 12000; i++)
            //    {
            //        var item = new DocumentDto()
            //        {
            //            Content = Content(),
            //            CreateDate = DateTime.Now.AddMinutes(-i),
            //            Important = false,IsActive = true,
            //            Notifications = "Thong bao",
            //            IdUser = (int) _fn.User().Id,
            //            Author = _fn.User().FullName,
            //            Url = Content()+"pdf"
            //        };
            //        _documentService.CreateOrUpdate(item);

            //    }
            //}

            return View();
        }

        public JsonResult GetIsAdmin()
        {
            var id = _userManager.AbpSession.UserId;
            return Json(_fn.IsAdmin((long) id));
        }
        [HttpPost]
        public  IActionResult UploadFile(IList<IFormFile> files)
        {
          
           
            var entity = new DocumentDto()
            {
                CreateDate = DateTime.Now,
                IdUser = (int) _fn.User().Id,
                Author = _fn.User().UserName,
                Important = false,
                IsActive = true,
                Url = _fn.UploadFile(files)
            };

            return Json(entity);
        }

        //[HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var dt = await _documentService.GetId(id);
            //return Json(dt);
            return View("_Edit", dt);
        }
        public async Task<ActionResult> _Edit(int id)
        {
            var dt = await _documentService.GetId(id);
            //return Json(dt);
            return View("_Edit", dt);
        }
        [HttpPost]
        public IActionResult _Edit(Document.Document entity)
        {
            entity.Author = _fn.User().UserName;
            entity.IdUser = (int)_fn.User().Id;
            _documentService.Update(entity);
            //return View("Index");
            return RedirectToAction("Index", "Document");
        }
        [HttpPost]
        public IActionResult Edit(Document.Document entity)
        {
            entity.Author = _fn.User().UserName;
            entity.IdUser = (int)_fn.User().Id;
            _documentService.Update(entity);
            return Json("");
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                    {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private string Content()
        {
            var contents =
                @"The problem is that LINQ+Entity Framework has probably been creating a new query for every request because it's been inserting the values for pos and size as literals into the query. Instead of recycling the query (which really hasn't changed from one request to another), LINQ+EF has been re-analyzing the expression tree for your LINQ statement and submitting a new SQL statement on each request.";
            var s = "";
            var n = contents.Split(" ").Length;
             var r = new Random();
            for (int i = 0; i < r.Next(4,n); i++)
            {
                s += contents.Split(" ")[i]+" ";
            }
            return s;
        }
        [HttpGet]
        public IActionResult Search(Loads loads)
        {
            var take = loads.Take;
            var skip = loads.Skip;
            var name = loads.Search;
            var data = _documentService.Search(skip, take, name);
            return Json(data);
        }
        [HttpGet]
        public  JsonResult GetAll()
        {
            var dt = _documentService.GetAll();
            return Json(dt);
        }
        [HttpPost]
        public IActionResult Create(DocumentDto entity)
        {
            try
            {
                entity.Author = _fn.User().UserName;
                entity.IdUser = (int) _fn.User().Id;
                _documentService.CreateOrUpdate(entity);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Json("0");
            }
        }
        [HttpPost]
        public IActionResult EditIportant(int id)
        {
            var item = _documentService.GetId(id).Result;
            if (ModelState.IsValid)
            {
                item.Important = !item.Important;
                _documentService.Update(item);
            }
            return Json(item.Important);
        }
       

        //[HttpDelete]
        public IActionResult Delete(int id)
        {
            _documentService.Delete(id);
            return  Redirect("/Document");
        }

        [HttpGet]
        public JsonResult LoadCatalogue()
        {
            return Json(_documentService.LoadCatalogue());
        }
        //--------------------------------


    }
}