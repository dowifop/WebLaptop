use master
if exists (select*from sysdatabases where name = 'WebLaptop')
drop database WebLaptop
go 
create database WebLaptop
go
use WebLaptop
go
Create table Admin
(
	MaAd INT NOT NULL IDENTITY(1,1) ,
	TaiKhoan nvarchar(100) NOT NULL ,
	MatKhau nvarchar(100) NOT NULL,
	TenAd nvarchar(100) NOT NULL,
	primary key (MaAD)
);
CREATE TABLE KhachHang
(
  MaKH nvarchar(50) NOT NULL  ,
  TenKH nvarchar(50) NOT NULL,
  Email nvarchar(50) NOT NULL,
  SDT nchar(10) NOT NULL,
  DiaChi nvarchar(100) NOT NULL,
  MatKhau nvarchar(100) NOT NULL,
  PRIMARY KEY (MaKH)
);
	

Create table Loai
(
	maLoai int NOT NULL identity,
	tenLoai nvarchar(50) NOT NULL,
	primary key(maLoai)
);
Create table NSX
(
	maNSX int identity NOT NULL ,
	tenNSX nvarchar(100) NOT NULL,
	primary key (maNSX)
);
Create table SanPham
(
	maLaptop int Identity(1,1) NOT NULL,
	tenLaptop nvarchar(100) NOT NULL,
	gia numeric(18,0) NOT NULL,
	Mota nvarchar(max) NOT NULL,
	Hinh nvarchar(max) NOT NULL,
	maLoai int NOT NULL,
	maNSX int NOT NULL,
	SoLuong int NOT NULL,
	CPU nvarchar(100),
	VGA nvarchar(100),
	RAM nvarchar(100),
	SSD nvarchar(100),
	primary key (maLaptop),
	FOREIGN KEY (maLoai) REFERENCES Loai(maLoai),
	FOREIGN KEY (maNSX) REFERENCES NSX(maNSX)
);


CREATE TABLE DonHang
(
  MaDH INT NOT NULL IDENTITY(1,1),
  DiaChi nvarchar(200) NOT NULL,
  DienThoaiNhan nchar(10) NOT NULL,
  TinhTrang nvarchar(50) ,
  TongTien NUMERIC(18) NOT NULL,
  NgayDH DATE NOT NULL,
  TenNguoiNhan nvarchar(100) NOT NULL,
  EmailNguoNhan nvarchar(100) NOT NULL,
  MaKH nvarchar(50) NOT NULL,
  PRIMARY KEY (MaDH),
  FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH)
);
CREATE TABLE CTDH
(
  MaCTDH INT NOT NULL IDENTITY(1,1),
  Gia NUMERIC(18) NOT NULL,
  SoLuong INT NOT NULL,
  MaDH INT NOT NULL,
  maLaptop INT NOT NULL,
  PRIMARY KEY (MaCTDH),
  FOREIGN KEY (MaDH) REFERENCES DonHang(MaDH),
  FOREIGN KEY (maLaptop) REFERENCES SanPham(maLaptop)
);

--NSX
insert into NSX values('Asus')
insert into NSX values('Dell')
insert into NSX values('HP')
insert into NSX values('MSI')
insert into NSX values ('Acer')
select * from NSX
--Loai
insert into Loai values(N'Thường')
insert into Loai values(N'Mini')
insert into Loai values(N'Cao')
-- Sản phẩm của Asus (maNSX = 1)
INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop Asus ROG Strix G15', 25000000, N'Model gaming mạnh mẽ từ Asus, với card đồ họa RTX 3060 và vi xử lý Intel Core i7', '/Content/image/asus_rog.png', 1, 1, 50);

INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop Asus VivoBook S15', 18000000, N'Laptop phổ thông với màn hình 15.6 inch, vi xử lý Intel Core i5', '/Content/image/asus_vivobook.png', 1, 1, 30);

INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop Asus TUF Gaming', 21000000, N'Laptop gaming tầm trung với card đồ họa GTX 1660 Ti và vi xử lý AMD Ryzen 5', '/Content/image/asus_tuf.png', 1, 1, 40);

INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop Asus ZenBook Duo', 28000000, N'Laptop đa nhiệm với màn hình kép và vi xử lý Intel Core i7', '/Content/image/asus_zenbook_duo.png', 3, 1,  25);

-- Sản phẩm của Dell (maNSX = 2)
INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop Dell XPS 13 9305', 23000000, N'Laptop ultrabook mỏng nhẹ, chất liệu cao cấp, màn hình 13.3 inch Full HD', '/Content/image/dell_xps.png', 2, 2,  60);

INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop Dell Inspiron 15', 19000000, N'Laptop phổ thông với màn hình 15.6 inch, vi xử lý Intel Core i3', '/Content/image/dell_inspiron.png', 1, 2,  35);
INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop Dell XPS 15', 28000000, N'Laptop cao cấp với màn hình 15 inch 4K, vi xử lý Intel Core i9', '/Content/image/dell_xps_15.png', 1, 2,  15);

INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop Dell Inspiron 13', 19000000, N'Laptop phổ thông với màn hình 13.3 inch, vi xử lý Intel Core i5', '/Content/image/dell_inspiron_13.png', 3, 2,  20);

-- Sản phẩm của HP (maNSX = 3)
INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop HP Pavilion 15', 18000000, N'Laptop học tập và giải trí tốt, màn hình 15.6 inch, vi xử lý Intel Core i5', '/Content/image/hp_pavilion.png', 1, 3,  70);
INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop HP Spectre x360', 32000000, N'Laptop 2 trong 1 cao cấp với màn hình cảm ứng 13.3 inch, vi xử lý Intel Core i7', '/Content/image/hp_spectre_x360.png', 2, 3,  10);

INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX, SoLuong)
VALUES(N'Laptop HP Envy 15', 25000000, N'Laptop đa nhiệm với màn hình 15.6 inch, vi xử lý AMD Ryzen 7', '/Content/image/hp_envy_15.png', 1, 3,  15);

-- Sản phẩm của MSI (maNSX = 4)
INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop MSI Prestige 14', 27000000, N'Laptop cho những người làm việc chuyên nghiệp, thiết kế đẹp, màn hình sắc nét', '/Content/image/msi_prestige.png', 1, 4,  40);
INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop MSI GS66 Stealth', 32000000, N'Laptop gaming siêu mạnh mẽ với card đồ họa RTX 3080 và vi xử lý Intel Core i9', '/Content/image/msi_gs66.png', 1, 4,  10);
INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop MSI Modern 14', 22000000, N'Laptop đa nhiệm với màn hình 14 inch, vi xử lý AMD Ryzen 7', '/Content/image/msi_modern_14.png', 3, 4,  20);
-- Sản phẩm của Acer (maNSX = 5)
INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop Acer Aspire 5', 15000000, N'Laptop đa dụng với mức giá phải chăng, màn hình 15.6 inch, vi xử lý AMD Ryzen 5', '/Content/image/acer_aspire.png', 1, 5,  50);

INSERT INTO SanPham(tenLaptop, gia, Mota, Hinh, maLoai, maNSX,  SoLuong)
VALUES(N'Laptop Acer Predator Helios 300', 26000000, N'Laptop gaming mạnh mẽ với card đồ họa RTX 3070 và vi xử lý Intel Core i7', '/Content/image/acer_predator.png', 1, 5,  30);

-- Thêm sản phẩm khác cho các hãng khác nếu cần
select * from SanPham

-- Cập nhật cho sản phẩm "Laptop Asus ROG Strix G15"
UPDATE SanPham 
SET CPU = 'Intel Core i7', VGA = 'RTX 3060', RAM = '16GB', SSD = '512GB'
WHERE maLaptop = 1;

-- Cập nhật cho sản phẩm "Laptop Asus VivoBook S15"
UPDATE SanPham 
SET CPU = 'Intel Core i5', VGA = 'Intel UHD Graphics', RAM = '8GB', SSD = '256GB'
WHERE maLaptop = 2;

-- Cập nhật cho sản phẩm "Laptop Asus TUF Gaming"
UPDATE SanPham 
SET CPU = 'AMD Ryzen 5', VGA = 'GTX 1660 Ti', RAM = '8GB', SSD = '512GB'
WHERE maLaptop = 3;

-- Cập nhật cho sản phẩm "Laptop Dell XPS 13 9305"
UPDATE SanPham 
SET CPU = 'Intel Core i5', VGA = 'Intel UHD Graphics', RAM = '8GB', SSD = '256GB'
WHERE maLaptop = 5;



-- Cập nhật cho sản phẩm "Laptop Asus ZenBook Duo"
UPDATE SanPham 
SET CPU = 'Intel Core i7', VGA = 'Intel Iris Xe Graphics', RAM = '16GB', SSD = '1TB'
WHERE maLaptop = 4;

-- Cập nhật cho sản phẩm "Laptop Dell Inspiron 15"
UPDATE SanPham 
SET CPU = 'Intel Core i3', VGA = 'Intel UHD Graphics', RAM = '8GB', SSD = '256GB'
WHERE maLaptop = 6;

-- Cập nhật cho sản phẩm "Laptop Dell XPS 15"
UPDATE SanPham 
SET CPU = 'Intel Core i9', VGA = 'NVIDIA GeForce GTX 1650 Ti', RAM = '32GB', SSD = '1TB'
WHERE maLaptop = 7;

-- Cập nhật cho sản phẩm "Laptop Dell Inspiron 13"
UPDATE SanPham 
SET CPU = 'Intel Core i5', VGA = 'Intel UHD Graphics', RAM = '8GB', SSD = '256GB'
WHERE maLaptop = 8;

-- Cập nhật cho sản phẩm "Laptop HP Pavilion 15"
UPDATE SanPham 
SET CPU = 'Intel Core i5', VGA = 'NVIDIA GeForce MX350', RAM = '8GB', SSD = '512GB'
WHERE maLaptop = 9;

-- Cập nhật cho sản phẩm "Laptop HP Spectre x360"
UPDATE SanPham 
SET CPU = 'Intel Core i7', VGA = 'Intel Iris Xe Graphics', RAM = '16GB', SSD = '512GB'
WHERE maLaptop = 10;

-- Cập nhật cho sản phẩm "Laptop HP Envy 15"
UPDATE SanPham 
SET CPU = 'AMD Ryzen 7', VGA = 'NVIDIA GeForce GTX 1650', RAM = '16GB', SSD = '512GB'
WHERE maLaptop = 11;

-- Cập nhật cho sản phẩm "Laptop MSI Prestige 14"
UPDATE SanPham 
SET CPU = 'Intel Core i7', VGA = 'NVIDIA GeForce GTX 1650 Max-Q', RAM = '16GB', SSD = '512GB'
WHERE maLaptop = 12;

-- Cập nhật cho sản phẩm "Laptop MSI GS66 Stealth"
UPDATE SanPham 
SET CPU = 'Intel Core i9', VGA = 'RTX 3080', RAM = '32GB', SSD = '1TB'
WHERE maLaptop = 13;

-- Cập nhật cho sản phẩm "Laptop MSI Modern 14"
UPDATE SanPham 
SET CPU = 'AMD Ryzen 7', VGA = 'AMD Radeon Graphics', RAM = '16GB', SSD = '512GB'
WHERE maLaptop = 14;

-- Cập nhật cho sản phẩm "Laptop Acer Aspire 5"
UPDATE SanPham 
SET CPU = 'AMD Ryzen 5', VGA = 'AMD Radeon Graphics', RAM = '8GB', SSD = '256GB'
WHERE maLaptop = 15;

-- Cập nhật cho sản phẩm "Laptop Acer Predator Helios 300"
UPDATE SanPham 
SET CPU = 'Intel Core i7', VGA = 'RTX 3070', RAM = '16GB', SSD = '512GB'
WHERE maLaptop = 16;

--Admin
INSERT INTO Admin VALUES(N'admin', N'0123456',N'Trương Thanh Bình');
--KhachHang
INSERT INTO KhachHang Values(N'dowifop',N'Trương Thanh Bình',N'binh@gmail.com','0123456789',N'123 cây chuối p6 q10','0123456')
select * from KhachHang

