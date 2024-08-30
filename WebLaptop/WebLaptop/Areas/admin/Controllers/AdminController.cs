using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLaptop.Models;

namespace WebLaptop.Areas.admin.Controllers
{
    public class AdminController : Controller
    {
        WebLaptopEntities db = new WebLaptopEntities();
        // GET: admin/Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login");
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(admin.TaiKhoan))
                    ModelState.AddModelError(string.Empty, "User name không được để trống");
                if (string.IsNullOrEmpty(admin.MatKhau))
                    ModelState.AddModelError(string.Empty, "Password không được để trống");
                //Kiểm tra có admin này hay chưa
                var adminDB = db.Admins.FirstOrDefault(ad => ad.TaiKhoan == admin.TaiKhoan && ad.MatKhau == admin.MatKhau);
                if (adminDB == null)
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng");
                else
                {
                    Session["Admin"] = adminDB;
                    ViewBag.ThongBao = "Đăng nhập admin thành công";
                    return RedirectToAction("Index", "NSX");
                }
            }
            return View();
        }
        //public ActionResult Logout()
        //{
        //    Session["Admin"] = "";
        //    return Redirect("Login");
        //}
    }
}