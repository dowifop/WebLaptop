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
    
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            this.CTDHs = new HashSet<CTDH>();
        }
    
        public int maLaptop { get; set; }
        public string tenLaptop { get; set; }
        public decimal gia { get; set; }
        public string Mota { get; set; }
        public string Hinh { get; set; }
        public int maLoai { get; set; }
        public int maNSX { get; set; }
        public int SoLuong { get; set; }
        public string CPU { get; set; }
        public string VGA { get; set; }
        public string RAM { get; set; }
        public string SSD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CTDH> CTDHs { get; set; }
        public virtual Loai Loai { get; set; }
        public virtual NSX NSX { get; set; }
    }
}
