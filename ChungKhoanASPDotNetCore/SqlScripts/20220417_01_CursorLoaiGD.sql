CREATE OR ALTER PROCEDURE [dbo].[CursorLoaiGD]
  @OutCrsr CURSOR VARYING OUTPUT, 
  @macp nchar(20),  @LoaiGD bit 
AS
SET DATEFORMAT DMY 
----mua = 1----
IF (@LoaiGD = 1) 
  SET @OutCrsr=CURSOR KEYSET FOR 
  SELECT iD, SOLUONG, GiaDat FROM LENHDAT 
  WHERE MaCK=@macp and 
     CAST(NgayDat as DATE) = CAST(getdate() as DATE)
     AND LoaiGiaoDich=@LoaiGD AND SOLUONG >0  
    ORDER BY GiaDat DESC, NgayDat
ELSE
  SET @OutCrsr=CURSOR KEYSET FOR 
  SELECT iD, SOLUONG, GiaDat FROM LENHDAT 
  WHERE MaCK=@macp and 
     CAST(NgayDat as DATE) = CAST(getdate() as DATE)
     AND LoaiGiaoDich=@LoaiGD AND SOLUONG >0  
    ORDER BY GiaDat, NgayDat
OPEN @OutCrsr