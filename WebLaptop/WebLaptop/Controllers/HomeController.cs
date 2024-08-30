using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLaptop.Models;

namespace WebLaptop.Controllers
{
    
    public class HomeController : Controller
    {
        private WebLaptopEntities db = new WebLaptopEntities();
        public ActionResult Index()
        {
            var sanphams = db.SanPhams.ToList();
            return View(sanphams);
        }

     

        public ActionResult Details(int id)
        {
            var sanphams =db.SanPhams.FirstOrDefault(s => s.maLaptop == id);
            var relatedProducts = db.SanPhams.Where(p => p.maNSX == sanphams.maNSX && p.maLaptop != sanphams.maLaptop).ToList();
            ViewBag.RelatedProducts = relatedProducts;
            return View(sanphams);
        }
        public ActionResult TheoHang(int maNSX)
        {
            var sanPhamsTheoHang = db.SanPhams.Where(s => s.maNSX == maNSX).ToList();
            ViewBag.LoaiSanPham = new SelectList(db.Loais, "maLoai", "tenLoai");
            ViewBag.maNSX = maNSX;
            return View(sanPhamsTheoHang);
        }

        [HttpGet]
        public ActionResult LaySanPhamTheoLoai(int maLoai, int maNSX)
        {
            var sanPhamsTheoLoai = db.SanPhams.Where(s => s.maLoai == maLoai && s.maNSX == maNSX).ToList();

          
            return PartialView("_SanPhamPartial", sanPhamsTheoLoai);
        }

    }
}