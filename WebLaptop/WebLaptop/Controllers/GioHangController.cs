using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLaptop.Models;

namespace WebLaptop.Controllers
{
    public class GioHangController : Controller
    {
        WebLaptopEntities db = new WebLaptopEntities();
        // GET: GioHang
        public List<cart> LayGioHang()
        {
            List<cart> gioHang = Session["GioHang"] as List<cart>;
            //Nếu giỏ hàng chưa có thì tạo mới và đưa váo session
            if(gioHang == null )
            {
                gioHang= new List<cart>();
                Session["GioHang"]=gioHang;
            }
            return gioHang;
        }
        public ActionResult ThemSanPham(int MaSP)
        {
            //lấy giỏ hàng hiện tại 
            List<cart> gioHang = LayGioHang();
            //Kiểm tra xem có tồn tại mặt hàng trong giỏ hay chưa
            //Nếu có thì tăng lên 1,ngược lại thêm vào giỏ
            cart sanpham = gioHang.FirstOrDefault(s => s.maLaptop == MaSP);
            if( sanpham == null )
            {
                sanpham= new cart(MaSP);
                gioHang.Add(sanpham);
            }
            else
            {
                sanpham.SoLuong++;//Sản phẩm đã có trong giỏ thì tăng lên 1
            }
          
            return RedirectToAction("Details", "Home", new {id = MaSP });
        }
        private int TinhTongSL()
        {
            int tongSL = 0;
            List<cart> gioHang= LayGioHang();
            if (gioHang != null)
                tongSL = gioHang.Sum(sp => sp.SoLuong);
            return tongSL;
        }

        private decimal TinhTongTien()
        {
            decimal tongTien = 0;
            List<cart> gioHang = LayGioHang();
            if (gioHang != null)
                tongTien = gioHang.Sum(sp => sp.ThanhTien());
            return tongTien;
        }
        public ActionResult HienThiGioHang()
        {
            List<cart> gioHang = LayGioHang();
            //Nếu giỏ hàng trống thì quay lại ban đầu
            if(gioHang == null || gioHang.Count == 0) { 
                return RedirectToAction("Index","Home"); 
            }
            ViewBag.tongSL = TinhTongSL(); ;
            ViewBag.tongTien = TinhTongTien().ToString("N0") + "₫";
            return View(gioHang);//Trả về view hiển thị thông tin giỏ hàng
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.tongSL = TinhTongSL();
            ViewBag.tongTien = TinhTongTien().ToString("N0") + "₫";
            return PartialView();
        }
        public ActionResult TangSoLuong(int MaSP)
        {
            List<cart> gioHang = LayGioHang();
            cart sanpham = gioHang.FirstOrDefault(s => s.maLaptop == MaSP);
            if (sanpham != null)
            {
                
                    sanpham.SoLuong++;
                
            }
            return RedirectToAction("HienThiGioHang");
        }

        public ActionResult GiamSoLuong(int MaSP)
        {
            List<cart> gioHang = LayGioHang();
            cart sanpham = gioHang.FirstOrDefault(s => s.maLaptop == MaSP);
            if (sanpham != null && sanpham.SoLuong > 1)
            {
                sanpham.SoLuong--;
            }
            else if (sanpham != null && sanpham.SoLuong == 1)
            {
                gioHang.Remove(sanpham);
            }
            return RedirectToAction("HienThiGioHang");
        }
        public ActionResult XoaSanPham(int MaSP)
        {
            List<cart> gioHang = LayGioHang();
            cart sanpham = gioHang.FirstOrDefault(s => s.maLaptop == MaSP);
            if (sanpham != null)
            {
                gioHang.Remove(sanpham);
            }
            return RedirectToAction("HienThiGioHang");
        }
       public ActionResult DatHang()
            {
                if (Session["KH"] == null) //Chưa đăng nhập
                    return RedirectToAction("Login", "Account");
            List<cart> gioHang = LayGioHang();
            if (gioHang == null || gioHang.Count == 0)//Chưa có ior hàng hoặc chưa có sản phẩm
                return RedirectToAction("Index", "Home");

            ViewBag.tongSL = TinhTongSL(); ;
            ViewBag.tongTien = TinhTongTien().ToString("N0") + "₫";
            return View(gioHang); 

        }
        public ActionResult DongYDatHang()
        {
            KhachHang kh = Session["KH"] as KhachHang;
            List<cart> gioHang = LayGioHang();

            DonHang donHang = new DonHang();
            donHang.MaKH = kh.MaKH;
            // Chuỗi ngày tháng theo định dạng "dd/MM/yyyy"
            string ngayThangStr = DateTime.Now.ToString("dd/MM/yyyy");

            // Chuyển đổi chuỗi thành kiểu DateTime
            DateTime ngayThang = DateTime.ParseExact(ngayThangStr, "dd/MM/yyyy", null);

            // Gán giá trị cho thuộc tính 
            donHang.NgayDH = ngayThang;
            donHang.TongTien =TinhTongTien();
            donHang.TenNguoiNhan = kh.TenKH;
            donHang.DienThoaiNhan = kh.SDT;
            donHang.DiaChi =kh.DiaChi;
            donHang.TinhTrang = "Chưa xử lý";
            donHang.EmailNguoNhan = kh.Email;

            db.DonHangs.Add(donHang);
            db.SaveChanges();
            //Lần lượt thêm từng chi tiết cho đơn hàng trên
            foreach (var sanpham in gioHang)
            {
                CTDH chitiet = new CTDH(); 
                chitiet.MaDH = donHang.MaDH;
                chitiet.maLaptop = sanpham.maLaptop;
                chitiet.SoLuong =sanpham.SoLuong;
                chitiet.Gia = donHang.TongTien;
                db.CTDHs.Add(chitiet);
            }
            db.SaveChanges();
            //Xóa giỏ hàng 
            Session["GioHang"] = null;
            return RedirectToAction("HoanThanhDonHang");
        }
        public ActionResult HoanThanhDonHang()
        {
            return View();
        }
    }
}