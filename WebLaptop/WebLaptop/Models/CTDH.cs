//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebLaptop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CTDH
    {
        public int MaCTDH { get; set; }
        public decimal Gia { get; set; }
        public int SoLuong { get; set; }
        public int MaDH { get; set; }
        public int maLaptop { get; set; }
    
        public virtual DonHang DonHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}
