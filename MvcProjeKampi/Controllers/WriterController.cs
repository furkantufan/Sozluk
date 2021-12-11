using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinesLayer.Concrete;
using BussinesLayer.ValidationRules_Fluent_Validation;
using DataAccesLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;

namespace WebSozluk.Controllers
{
    [AllowAnonymous]

    public class WriterController : Controller
    {
        WriterManager wm = new WriterManager(new EfWriterDal());
        WriterValidator writervalidator = new WriterValidator();

        public ActionResult Index()
        {
            var WriterValues = wm.GetList();
            return View(WriterValues);
        }
        [HttpGet]
        public ActionResult AddWriter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddWriter(Writer p)
        {
            ValidationResult results = writervalidator.Validate(p);
            if (results.IsValid)
            {
                p.WriterStatus = true;
                wm.WriterAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditWriter(int id)
        {
            var writerValue = wm.GetById(id);
            return View(writerValue);
        }
        [HttpPost]
        public ActionResult EditWriter(Writer p)
        {
            ValidationResult results = writervalidator.Validate(p);
            if (results.IsValid)
            {
                p.WriterStatus = true;
                wm.WriterUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}

































//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using BussinesLayer.Abstract;
//using BussinesLayer.Concrete;
//using BussinesLayer.ValidationRules_Fluent_Validation;
//using DataAccesLayer.EntityFramework;
//using EntityLayer.Concrete;
//using EntityLayer.Dto;
//using FluentValidation.Results;

//namespace WebSozluk.Controllers
//{
//    public class WriterController : Controller
//    {
//        WriterManager wm = new WriterManager(new EfWriterDal());
//        WriterValidator writervalidator = new WriterValidator();
//        IAuthService authService = new AuthManager(new AdminManager(new EfAdminDal()), new WriterManager(new EfWriterDal()));

//        public ActionResult Index()
//        {
//            var WriterValues = wm.GetList();
//            return View(WriterValues);
//        }
//        [HttpGet]
//        public ActionResult AddWriter()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult AddWriter(WriterLoginDto p)
//        {
//            ValidationResult results = writervalidator.Validate(getWriterFromWriterDto(p));
//            if (results.IsValid)
//            {
//                authService.WriterRegister(p);
//                return RedirectToAction("Index");
//            }
//            else
//            {
//                foreach (var item in results.Errors)
//                {
//                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
//                }
//            }
//            return View();
//        }
//        [HttpGet]
//        public ActionResult EditWriter(int id)
//        {
//            var writerValue = wm.GetById(id);
//            return View(writerValue);
//        }
//        [HttpPost]
//        public ActionResult EditWriter(Writer p)
//        {
//            ValidationResult results = writervalidator.Validate(p);
//            if (results.IsValid)
//            {
//                p.WriterStatus = true;
//                wm.WriterUpdate(p);
//                return RedirectToAction("Index");
//            }
//            else
//            {
//                foreach (var item in results.Errors)
//                {
//                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
//                }
//            }
//            return View();
//        }


//        //private Writer getWriterFromWriterDto(WriterLoginDto WriterLoginDto)
//        //{
//        //    Writer writer = new Writer();
//        //    writer.WriterName = WriterLoginDto.WriterName;
//        //    writer.WriterSurname = WriterLoginDto.WriterSurname;
//        //    writer.WriterMail = WriterLoginDto.WriterMail;
//        //    writer.WriterAbout = WriterLoginDto.WriterAbout;
//        //    writer.WriterTitle = WriterLoginDto.WriterTitle;
//        //    writer.WriterImage = WriterLoginDto.WriterImage;
//        //    return writer;


//        //}
//    }
//}
