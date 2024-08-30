using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLaptop.Models;

namespace WebLaptop.Areas.admin.Controllers
{
    public class NSXController : Controller
    {
        WebLaptopEntities db = new WebLaptopEntities();
        // GET: admin/NSX
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            return View(db.NSXes.ToList());
        }
        public ActionResult Loai()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            return View(db.Loais.ToList());
        }
    }
}