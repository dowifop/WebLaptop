using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebLaptop.Models;

namespace WebLaptop.Areas.admin.Controllers
{
    public class KhachHangController : Controller
    {
        private WebLaptopEntities db = new WebLaptopEntities();
        // GET: KhachHang
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            return View(db.KhachHangs.ToList());
        }

        // GET: KhachHang/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }
        public ActionResult Add()
        {
            return View();
        }

        // POST: KhachHang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "MaKH,MatKhau,TenKH,DiaChi,SDT,Email")] KhachHang tblKhachHang)
        {
            if (ModelState.IsValid)
            {
                if (db.KhachHangs.Any(k => k.MaKH == tblKhachHang.MaKH))
                {
                    ModelState.AddModelError("", "Tên tài khoản đã được sử dụng!");
                    return View(tblKhachHang);
                }

                if (tblKhachHang.MatKhau.Length < 6)
                {
                    ModelState.AddModelError("MatKhau", "Mật khẩu phải chứa ít nhất 6 kí tự.");
                    return View(tblKhachHang);
                }

                if (!tblKhachHang.Email.EndsWith("@gmail.com"))
                {
                    ModelState.AddModelError("Email", "Email phải kết thúc bằng '@gmail.com'.");
                    return View(tblKhachHang);
                }

                // kiểm tra số điện thoại phải bắt đầu bằng số 0 và có 10 chữ số từ 0 -> 9
                if (!Regex.IsMatch(tblKhachHang.SDT, "^0\\d{8,}$"))
                {
                    ModelState.AddModelError("SDT", "Số điện thoại phải bắt đầu bằng số 0 và có ít nhất 9 chữ số.");
                    return View(tblKhachHang);
                }
                if (db.KhachHangs.Find(tblKhachHang.MaKH) == null)
                {
                    db.KhachHangs.Add(tblKhachHang);
                    db.SaveChanges();
                    return RedirectToAction("Index", "KhachHang");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }

            return View(tblKhachHang);
        }

        // GET: KhachHang/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang tblKhachHang = db.KhachHangs.Find(id);
            if (tblKhachHang == null)
            {
                return HttpNotFound();
            }
            return View(tblKhachHang);
        }

        // POST: KhachHang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,MatKhau,TenKH,DiaChi,SDT,Email")] KhachHang tblKhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblKhachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblKhachHang);
        }
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang tblKhachHang = db.KhachHangs.Find(id);
            if (tblKhachHang == null)
            {
                return HttpNotFound();
            }
            return View(tblKhachHang);
        }

        // POST: KhachHang/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                KhachHang tblKhachHang = db.KhachHangs.Find(id);
                db.KhachHangs.Remove(tblKhachHang);
                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

    }
}
