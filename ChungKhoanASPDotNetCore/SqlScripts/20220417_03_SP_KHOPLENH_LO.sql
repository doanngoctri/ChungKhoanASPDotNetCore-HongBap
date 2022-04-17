CREATE OR ALTER PROC [dbo].[SP_KHOPLENH_LO]
 @macp nchar(20),  @LoaiGD bit, 
 @soluongMB INT, @giadatMB FLOAT,@iiid int
AS
DECLARE @CrsrVar CURSOR , @idlenhdat INT, @soluong INT, @giadat FLOAT 
,@soluongkhop INT, @giakhop FLOAT, @soluonggoc INT,@idlenhbd int,@giaBG float,@KLBG int
set @idlenhbd = @iiid;
set @giaBG = 0; --- Giá loop để lưu trữ giá khớp trước đó. GiaKhop chỉ đúng trong scope
set @KLBG = 0; --- Khối lượng khớp tất cả
SET DATEFORMAT DMY
set @LoaiGD = ~@LoaiGD
EXEC CursorLoaiGD  @CrsrVar OUTPUT, @macp, @LoaiGD
set @LoaiGD = ~@LoaiGD

SET @soluonggoc = @soluongMB
FETCH NEXT FROM @CrsrVar  INTO  @idlenhdat , @soluong , @giadat
--SELECT @ngaydat , @soluong , @giadat
WHILE (@@FETCH_STATUS <> -1 AND @soluongMB > 0)
BEGIN
 IF  ((@LoaiGD = 0 and @giadatMB <= @giadat) 
 or (@LoaiGD = 1 and @giadatMB >= @giadat))
  BEGIN
	declare @compare int,@MaTTT nchar(20),@value int
	set @value = 0
	set @MaTTT = 'KH'
	set @soluongkhop = @soluong
	set @compare = @soluongMB - @soluong
	set @giakhop = @giadat
	if(@compare<0)
	begin
		set @value = @soluong - @soluongMB
		set @MaTTT = 'CK'
		set @soluongkhop = @soluongMB
		set @compare = 0
	end
	UPDATE dbo.LENHDAT  
         SET SOLUONG = @value, TrangThai = @MaTTT
         WHERE CURRENT OF @CrsrVar
	set @soluongMB = @compare
	---Khối lượng khớp
	 if(@giaBG = @giakhop)
		set @KLBG = @KLBG + @soluongkhop
	 else
	 begin
		set @KLBG = @soluongkhop
		set @giaBG = @giakhop
	 end
	-- Cập nhật table LENHKHOP
	 INSERT INTO LenhKhop(NgayKhop, SoLuongKhop, GiaKhop, IdLenhDat) 
	 VALUES (GETDATE(), @soluongkhop, @giakhop, @idlenhdat)
	 INSERT INTO LenhKhop(NgayKhop, SoLuongKhop, GiaKhop, IdLenhDat) 
	 VALUES (GETDATE(), @soluongkhop, @giakhop, @idlenhbd)
  END
  FETCH NEXT FROM @CrsrVar INTO  @idlenhdat , @soluong , @giadat  
END
IF @soluongMB < @soluonggoc
BEGIN
	update LenhDat set SoLuong = @soluongMB,TrangThai = CASE WHEN @soluongMB = 0 THEN 'KH' ELSE 'CK' END
	where Id = @idlenhbd
	--- Gom khối lượng trên bảng điện
	declare @giaTrenBang float, @KLTrenBang int
	select @giaTrenBang = GiaKhop, @KLTrenBang=SoLuongKhop from BangGiaTrucTuyen where MaCK = @macp
	if(@giaTrenBang <> @giaBG or isnull(@KLTrenBang,0) = 0)
		set @KLTrenBang = 0
	update dbo.BangGiaTrucTuyen set GiaKhop = @giaBG,SoLuongKhop = @KLTrenBang+ @KLBG where MaCK = @macp
	set @LoaiGD = ~@LoaiGD
	EXEC sp_bgtt @macp,@LoaiGD
	set @LoaiGD = ~@LoaiGD
END
if @soluongMB <> 0
	EXEC sp_bgtt @macp,@LoaiGD
declare @sum int
select @sum = sum(k.SoLuongKhop) from LenhKhop k join LenhDat d on k.IdLenhDat = d.Id 
where  CAST(k.NgayKhop as DATE) = CAST(getdate() as DATE) and d.MaCK = @macp
update dbo.BangGiaTrucTuyen set TongSo = @sum/2 where MaCK = @macp
CLOSE @CrsrVar
DEALLOCATE @CrsrVar