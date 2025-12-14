using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StickyNotes.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Sticky Notes - Tu aplicación para organizar tus notas";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Información de contacto";
            return View();
        }
    }
}