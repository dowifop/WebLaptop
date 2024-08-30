using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebLaptop.Models;
using PagedList;
using System.Diagnostics;

namespace WebLaptop.Areas.admin.Controllers
{
    public class SanPhamController : Controller
    {
        private WebLaptopEntities db = new WebLaptopEntities();

        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null)
                return RedirectToAction("Login", "Admin");
            var products = db.SanPhams.ToList();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/SanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: Admin/SanPham/Create
        public ActionResult Create()
        {
            ViewBag.maLoai = new SelectList(db.Loais, "maLoai", "tenLoai"); 
            ViewBag.maNSX = new SelectList(db.NSXes, "maNSX", "tenNSX");
            return View();
        }

        // POST: Admin/SanPham/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "maLaptop,tenLaptop,gia,Mota,Hinh,maLoai,maNSX,SoLuong,CPU,VGA,RAM,SSD")] SanPham sanPham, HttpPostedFileBase Hinh)
        {
            if (Hinh != null && Hinh.ContentLength > 0)
            {
                string _fileName = Path.GetFileName(Hinh.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/image/"), _fileName);
                Hinh.SaveAs(path);
                sanPham.Hinh = "/Content/image/" + _fileName;
            }

            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maLoai = new SelectList(db.Loais, "maLoai", "tenLoai", sanPham.maLoai);
            ViewBag.maNSX = new SelectList(db.NSXes, "maNSX", "tenNSX", sanPham.maNSX);
            return View(sanPham);
        }

        // GET: Admin/SanPham/Edit/5

        public ActionResult Edit(int id)
        {
            var sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            Debug.WriteLine("maLoai: " + sanPham.maLoai);
            Debug.WriteLine("maNSX: " + sanPham.maNSX);
            ViewBag.maLoai = new SelectList(db.Loais, "maLoai", "tenLoai", sanPham.maLoai);
            ViewBag.maNSX = new SelectList(db.NSXes, "maNSX", "tenNSX", sanPham.maNSX);

            return View(sanPham);
        }

        // POST: Admin/SanPham/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maLaptop,tenLaptop,gia,Mota,Hinh,maLoai,maNSX,SoLuong,CPU,VGA,RAM,SSD")] SanPham sanpham, HttpPostedFileBase Hinh)
        {
            if (Hinh != null && Hinh.ContentLength > 0)
            {
                string _fn = Path.GetFileName(Hinh.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/image/"), _fn);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                Hinh.SaveAs(path);
                sanpham.Hinh = "~/Content/image/" + _fn;
            }
            else if (Hinh == null)
            {
                SanPham existingSanPham = db.SanPhams.Find(sanpham.maLaptop);
                if (existingSanPham != null)
                {
                    sanpham.Hinh = existingSanPham.Hinh;
                }
            }

            if (ModelState.IsValid)
            {
                var local = db.Set<SanPham>()
                         .Local
                         .FirstOrDefault(f => f.maLaptop == sanpham.maLaptop);
                if (local != null)
                {
                    db.Entry(local).State = EntityState.Detached;
                }

                db.Entry(sanpham).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maLoai = new SelectList(db.Loais.ToList(), "maLoai", "tenLoai", sanpham.maLoai);
            ViewBag.maNSX = new SelectList(db.NSXes.ToList(), "maNSX", "tenNSX", sanpham.maNSX);


            return View(sanpham);
        }



        // GET: Admin/SanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: Admin/SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }

}