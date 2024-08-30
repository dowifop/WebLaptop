using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebLaptop.Models;
using Microsoft.AspNet.Identity;


namespace WebLaptop.Controllers
{
    public class AccountController : Controller
    {
       WebLaptopEntities db = new WebLaptopEntities();
     
        public ActionResult Register()
        {
            var kh = new KhachHang();
            return View(kh);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include ="TenKH,MaKH,MatKhau,SDT,DiaChi,Email")] KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(kh.TenKH) ||
                    string.IsNullOrWhiteSpace(kh.MaKH) ||
                    string.IsNullOrWhiteSpace(kh.MatKhau) ||
                    string.IsNullOrWhiteSpace(kh.SDT) ||
                    string.IsNullOrWhiteSpace(kh.DiaChi) || 
                    string.IsNullOrWhiteSpace(kh.Email))
                {
                    ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                    return View(kh);
                }
                if (db.KhachHangs.Any(k => k.MaKH == kh.MaKH))
                {
                    ModelState.AddModelError("", "Tên tài khoản đã được sử dụng!");
                    return View(kh);
                }

                if (kh.MatKhau.Length < 6)
                {
                    ModelState.AddModelError("MatKhau", "Mật khẩu phải chứa ít nhất 6 kí tự.");
                    return View(kh);
                }

                if (!kh.Email.EndsWith("@gmail.com"))
                {
                    ModelState.AddModelError("Email", "Email phải kết thúc bằng '@gmail.com'.");
                    return View(kh);
                }

                // kiểm tra số điện thoại phải bắt đầu bằng số 0 và có 10 chữ số từ 0 -> 9
                if (!Regex.IsMatch(kh.SDT, "^0\\d{9,}$"))
                {
                    ModelState.AddModelError("SDT", "Số điện thoại phải bắt đầu bằng số 0 và có ít nhất 9 chữ số.");
                    return View(kh);
                }

                db.KhachHangs.Add(kh);
                db.SaveChanges();
                Session["KH"] = kh;
                return RedirectToAction("Login", "Account");
            }

            return View(kh);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHang kh)
        {
           if(ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(kh.MaKH))
                    ModelState.AddModelError("", "Vui lòng nhập tên đăng nhập");
                if (string.IsNullOrWhiteSpace(kh.MatKhau))
                    ModelState.AddModelError("", "Vui lòng nhập mật khẩu");
                if (ModelState.IsValid)
                {
                    
                    var user = db.KhachHangs.Where(a => a.MaKH.Equals(kh.MaKH) && a.MatKhau.Equals(kh.MatKhau)).FirstOrDefault();
                    if (user != null)
                    {
                        Session["KH"] = user;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Sai tên đăng nhập hoặc mật khẩu!");
                    }
                }
            }
           
            return View(kh);
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session["KH"] = null;
            KhachHang kh = (KhachHang)Session["KH"];
            if (kh != null)
                return RedirectToAction("Index", "Home");
            return View();
        }
        public ActionResult CaNhan()
        {
            KhachHang kh = new KhachHang();
            if (Session["KH"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                kh = (KhachHang)Session["KH"];
            }
            return View(kh);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CaNhan([Bind(Include = "TenKH,MaKH,MatKhau,SDT,DiaChi,Email")] KhachHang kh)
        {
            if (ModelState.IsValid)
            {

                if (kh.MatKhau.Length < 6)
                {
                    ModelState.AddModelError("MatKhau", "Mật khẩu phải chứa ít nhất 6 kí tự.");
                    return View(kh);
                }

                if (!kh.Email.EndsWith("@gmail.com"))
                {
                    ModelState.AddModelError("Email", "Email phải kết thúc bằng '@gmail.com'.");
                    return View(kh);
                }

                // kiểm tra số điện thoại phải bắt đầu bằng số 0 và có 10 chữ số từ 0 -> 9
                if (!Regex.IsMatch(kh.SDT, "^0\\d{8,}$"))
                {
                    ModelState.AddModelError("SDT", "Số điện thoại phải bắt đầu bằng số 0 và có ít nhất 9 chữ số.");
                    return View(kh);
                }
                db.Entry(kh).State = EntityState.Modified;
                db.SaveChanges();
                Session["KH"] = kh;
                return RedirectToAction("Index", "Home");
            }
            return View(kh);
        }
        public ActionResult Logout()
        {
            Session["KH"] = null;
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DanhSachDonHang()
        {
            KhachHang kh = (KhachHang)Session["KH"];
          

            string userId = kh.MaKH;
            var danhSachDonHang = db.DonHangs.Where(d => d.MaKH == userId).ToList();
            return View(danhSachDonHang);
        }

        public ActionResult ChiTietDonHang(int id)
        {
            KhachHang kh = (KhachHang)Session["KH"];
         

            string userId = kh.MaKH;
            var donHang = db.DonHangs.Include("CTDHs.SanPham").SingleOrDefault(d => d.MaDH == id && d.MaKH == userId);

            if (donHang == null)
            {
                return HttpNotFound();
            }
            decimal gia = donHang.CTDHs.Select(ctdh => ctdh.Gia).FirstOrDefault();
            ViewBag.Gia = gia;
            return View(donHang);
        }





    }
}