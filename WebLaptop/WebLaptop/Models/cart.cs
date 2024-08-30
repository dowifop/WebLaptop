using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLaptop.Models
{
    public class cart
    {
        WebLaptopEntities db = new WebLaptopEntities();
        public string Hinh { get; set; }
        public int  maLaptop { get; set; }
        public string tenLaptop { get; set; }
        public decimal gia { get; set; }
        public int SoLuong { get; set; }
        public int soLuongTrongKho { get; set; }

        public decimal ThanhTien()
        {
           
                return SoLuong * gia;
            
        }
        public cart(int maLaptop )
        {
            this.maLaptop = maLaptop;
            var sanpham = db.SanPhams.Single(s => s.maLaptop == this.maLaptop);
            this.tenLaptop = sanpham.tenLaptop; 
            this.Hinh = sanpham.Hinh;
            this.gia= decimal.Parse(sanpham.gia.ToString());
            this.SoLuong=1;
            this.soLuongTrongKho = sanpham.SoLuong;
        }
    }
}