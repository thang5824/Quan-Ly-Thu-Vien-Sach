CREATE DATABASE QuanLyThuVien
GO
USE QuanLyThuVien;

CREATE TABLE Sach (
    MaSach INT IDENTITY PRIMARY KEY,
    TenSach NVARCHAR(100) NOT NULL,
    TacGia NVARCHAR(100) NOT NULL
);

CREATE TABLE NhanVien (
    MaNV INT IDENTITY PRIMARY KEY, -- NV001, AD001
    TenNV NVARCHAR(50) NOT NULL,																																										
    SoDienThoai NVARCHAR(15) NOT NULL,
    LuongCoBan DECIMAL(18, 2) NOT NULL CHECK (LuongCoBan >=0),
);
CREATE TABLE TaiKhoan (
	id INT IDENTITY PRIMARY KEY,
	TenHienThi NVARCHAR(50),
    TenDangNhap NVARCHAR(50),
    MatKhau NVARCHAR(1000) NOT NULL,
    QuyenHan INT NOT NULL DEFAULT 0 -- 0 STAFF, 1ADMIN 
);

CREATE TABLE PhieuMuon (
    MaPhieuMuon INT IDENTITY PRIMARY KEY, -- HD001
    NgayLap DATE NOT NULL DEFAULT GETDATE(),
);

INSERT INTO PhieuMuon (NgayLap) VALUES
(GETDATE()),
(GETDATE()),
(GETDATE()),
(GETDATE()),
(GETDATE());
	

CREATE TABLE ChiTietPhieuMuon (
    MaChiTietPM INT IDENTITY PRIMARY KEY,
    idBook INT NOT NULL,
	idCheckOut INT NOT NULL,
    SoLuong INT NOT NULL DEFAULT 0,
	NgayTra Date Not null Default GetDate(),
);

CREATE TRIGGER trg_Update_NgayTra
ON ChiTietPhieuMuon
AFTER INSERT
AS
BEGIN
    UPDATE ctpm
    SET NgayTra = DATEADD(DAY, 7, pm.NgayLap)
    FROM ChiTietPhieuMuon ctpm
    JOIN PhieuMuon pm ON ctpm.idCheckOut = pm.MaPhieuMuon
    JOIN inserted i ON ctpm.MaChiTietPM = i.MaChiTietPM;
END;

INSERT INTO ChiTietPhieuMuon (idBook, idCheckOut, SoLuong)  
VALUES  
(2, 4, 1);  

INSERT INTO Sach (TenSach, TacGia) VALUES
(N'Python Cơ Bản', N'Nguyễn Văn A'),
(N'SQL Nâng Cao', N'Trần Thị B'),
(N'Giải Thuật', N'Lê Văn C'),
(N'Machine Learning', N'Phạm Thị D'),
(N'Node.js Web', N'Hoàng Văn E');

SELECT TenSach, TacGia FROM Sach WHERE MaSach = '2';

INSERT INTO NhanVien (TenNV, SoDienThoai, LuongCoBan)  
VALUES  
(N'Nguyễn Văn Hồ', '0123456789', 4000000),  
(N'Nguyễn Ngọc Quân', '0123456739', 5000000),  
(N'Cao Thị Kim Chi', '0123456781', 6000000),  
(N'Lê Anh Tuấn', '0123456785', 9000000),  
(N'Nguyễn Thị Mai', '0123456786', 5200000),  
(N'Nguyễn Văn An', '0123456787', 5400000),  
(N'Lê Quang Huy', '0123456788', 5050000),  
(N'Phan Thiên Vương', '0123456789', 5030000);  
go

CREATE PROC USP_GetEmployeeList
AS SELECT * FROM NhanVien
GO	

EXEC dbo.USP_GetEmployeeList
GO


INSERT INTO TaiKhoan (TenHienThi, TenDangNhap, QuyenHan, MatKhau)  
VALUES 
('Admin', 'Admin', 1, '123'); -- 1: ADMIN

-- Chèn dữ liệu cho các tài khoản nhân viên (mặc định QuyenHan là 0)
INSERT INTO TaiKhoan (TenHienThi, TenDangNhap, MatKhau)  
VALUES 
('Nhân viên 002', 'nhanvien002', '123'),
('Nhân viên 334', 'nhanvien334', '123'),
('Nhân viên 065', 'nhanvien065', '123'),
('Nhân viên 005', 'nhanvien005', '123'),
('Nhân viên 006', 'nhanvien006', '123'),
('Nhân viên 142', 'nhanvien142', '123'),
('Nhân viên 122', 'nhanvien122', '123')

select * from TaiKhoan where TenDangNhap = 'nhanvien122';

--store proc usp_gettaccountbyusername
Create PROC USP_GetAccountByUserName
@TenDangNhap NVARCHAR(50)
AS
BEGIN
	SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap
END
GO

EXEC USP_GetAccountByUserName @TenDangNhap = N'nhanvien142'
GO

--store proc usp_login
Create PROC USP_Login
@TenDangNhap NVARCHAR(50), @MatKhau NVARCHAR(1000)
AS
BEGIN
	SELECT * FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau
END
GO

--store proc SACH
CREATE PROC USP_GetBookList
AS SELECT * FROM Sach
GO	

EXEC dbo.USP_GetBookList
GO


INSERT INTO ChiTietPhieuMuon (idBook, idCheckOut, SoLuong)  
VALUES  
(5, 1, 1),  
(4, 2, 3),  
(3, 3, 2);  
GO



SELECT * FROM PhieuMuon WHERE MaPhieuMuon = '3'

SELECT * FROM ChiTietPhieuMuon WHERE MaChiTietPM = '2'; 


CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) RETURNS NVARCHAR(4000)
AS
BEGIN
	IF @strInput IS NULL RETURN @strInput
	IF @strInput = '' RETURN @strInput

	DECLARE @RT NVARCHAR(4000)
	DECLARE @SIGN_CHARS NCHAR(136)
	DECLARE @UNSIGN_CHARS NCHAR (136)

	SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' + NCHAR(272)+ NCHAR(208)
	SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'
	
	DECLARE @COUNTER int
	DECLARE @COUNTER1 int
	SET @COUNTER = 1

	WHILE (@COUNTER <= LEN(@strInput))
	BEGIN
		SET @COUNTER1 = 1
		WHILE (@COUNTER1 <= LEN(@SIGN_CHARS) + 1)
			BEGIN
				IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) )
				BEGIN
					IF @COUNTER=1
						SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1)
					ELSE
						SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER)
					BREAK
				END
					SET @COUNTER1 = @COUNTER1 +1
			END
			SET @COUNTER = @COUNTER +1
	END
	SET @strInput = replace(@strInput,' ','-')
	RETURN @strInput
END

SELECT MaChiTietPM, idBook, idCheckOut, SoLuong, NgayTra 
FROM ChiTietPhieuMuon;


