using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Abp.AutoMapper;
using AeDashboard.Authorization.Users;
using AeDashboard.Controllers;
using AeDashboard.Document;
using AeDashboard.Document.Dto;
using AeDashboard.Fn;
using AeDashboard.Web.Models.Loads;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AeDashboard.Web.Controllers
{
    public class DocumentController : AeDashboardControllerBase
    {
        private readonly IDocumentService _documentService;
        private readonly UserManager _userManager;
        private readonly IFn _fn;
        public DocumentController(IDocumentService documentService, UserManager userManager,IFn fn)
        {
            _documentService = documentService;
            _userManager = userManager;
            _fn = fn;
        }
        public IActionResult Index()
        {
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dt = _documentService.GetId(id);
            var k = dt.MapTo<DocumentDto>();
            return View("Edit",dt);
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
                return View("Index");
            }
            catch (Exception e)
            {
                return Json("0");
            }
        }

        public IActionResult GetId(int id)
        {
            //var item = _documentService.GetId(id);
            return View("_Create");
        }
        [HttpPost]
        public IActionResult Update(DocumentFileDto entity)
        {
            try
            {
                //_documentService.CreateOrUpdate(entity);
                return Json("1");
            }
            catch (Exception e)
            {
                return Json("0");
            }
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            try
            {
                _documentService.Delete(id);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }

        [HttpGet]
        public JsonResult LoadCatalogue()
        {
            return Json(_documentService.LoadCatalogue());
        }
        //--------------------------------


    }
}