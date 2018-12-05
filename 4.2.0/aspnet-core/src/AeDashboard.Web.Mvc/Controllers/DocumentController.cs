using System;
using System.Threading.Tasks;
using AeDashboard.Controllers;
using AeDashboard.Document;
using AeDashboard.Document.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AeDashboard.Web.Controllers
{
    public class DocumentController : AeDashboardControllerBase
    {
        //private readonly IDocumentService _documentService;

        //public DocumentController(IDocumentService documentService)
        //{
        //    _documentService = documentService;
        //}
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public  JsonResult GetAll()
        {
            //var dt = _documentService.GetAll();
            return Json("");
        }
        [HttpPost]
        public IActionResult Create(DocumentFileDto entity)
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
                //_documentService.Delete(id);
                return Json(true);
            }
            catch (Exception e)
            {
                return Json(false);
            }
        }
    }
}