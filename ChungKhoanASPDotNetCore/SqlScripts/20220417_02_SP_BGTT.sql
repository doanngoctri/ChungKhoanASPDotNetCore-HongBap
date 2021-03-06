CREATE OR ALTER PROCEDURE [dbo].[SP_BGTT]
@MACP nchar(20),
@LOAI_GD bit
AS
BEGIN
IF EXISTS ( SELECT  *
            FROM    tempdb.sys.tables
            WHERE   name = '#TEMP_MUA' )
    DROP TABLE #TEMP_MUA

IF EXISTS ( SELECT  *
            FROM    tempdb.sys.tables
            WHERE   name = '#TEMP_BAN' )
    DROP TABLE #TEMP_BAN
IF(@LOAI_GD = 1)
BEGIN
	SELECT TOP 3 MACK, GiaDat, SUM(ISNULL(SOLUONG, 0)) AS SL, ROW_NUMBER() OVER(ORDER BY GiaDat DESC) AS RN INTO #TEMP_MUA
	FROM dbo.LENHDAT 
	WHERE MACK = @MACP
		AND LoaiGiaoDich = @LOAI_GD and
		CAST(NgayDat as DATE) = CAST(getdate() as DATE) and (TrangThai = 'KP' or TrangThai = 'CK')
	GROUP BY MACK, GiaDat
	HAVING SUM(ISNULL(SOLUONG, 0)) > 0
	UPDATE dbo.BANGGIATRUCTUYEN
	SET GiaMua1 = NULL, SoLuongMua1 = NULL, GiaMua2 = NULL, SoLuongMua2 = NULL, GiaMua3 = NULL, SoLuongMua3 = NULL
	WHERE MACK = @MACP

	-- GIA 1
	IF EXISTS (SELECT * FROM #TEMP_MUA WHERE MaCK = @MACP AND RN = 1)
	BEGIN
		IF EXISTS (SELECT * FROM dbo.BANGGIATRUCTUYEN WHERE MaCK = @MACP)
		BEGIN
			UPDATE dbo.BANGGIATRUCTUYEN
			SET GiaMua1 = (SELECT GiaDat FROM #TEMP_MUA WHERE RN = 1),
				SoLuongMua1 = (SELECT SL FROM #TEMP_MUA WHERE RN = 1)
			WHERE MaCK = @MACP
		END
		ELSE
		BEGIN
			INSERT INTO dbo.BANGGIATRUCTUYEN
			        ( MaCK ,
			          GiaMua1 ,
			          SoLuongMua1 
			        )
			SELECT MaCK, GiaDat, SL FROM #TEMP_MUA WHERE RN = 1
		END
	END

	-- GIA 2
	IF EXISTS (SELECT * FROM #TEMP_MUA WHERE MaCK = @MACP AND RN = 2)
	BEGIN
		IF EXISTS (SELECT * FROM dbo.BANGGIATRUCTUYEN WHERE MaCK = @MACP)
		BEGIN
			UPDATE dbo.BANGGIATRUCTUYEN
			SET GiaMua2 = (SELECT GiaDat FROM #TEMP_MUA WHERE RN = 2),
				SoLuongMua2 = (SELECT SL FROM #TEMP_MUA WHERE RN = 2)
			WHERE MaCK = @MACP
		END
		ELSE
		BEGIN
			INSERT INTO dbo.BANGGIATRUCTUYEN
			        ( MaCK ,
			          GiaMua2 ,
			          SoLuongMua2 
			        )
			SELECT MaCK, GiaDat, SL FROM #TEMP_MUA WHERE RN = 2
		END
	END

	-- GIA 3
	IF EXISTS (SELECT * FROM #TEMP_MUA WHERE MaCK = @MACP AND RN = 3)
	BEGIN
		IF EXISTS (SELECT * FROM dbo.BANGGIATRUCTUYEN WHERE MaCK = @MACP)
		BEGIN
			UPDATE dbo.BANGGIATRUCTUYEN
			SET GiaMua3 = (SELECT GiaDat FROM #TEMP_MUA WHERE RN = 3),
				SoLuongMua3 = (SELECT SL FROM #TEMP_MUA WHERE RN = 3)
			WHERE MaCK = @MACP
		END
		ELSE
		BEGIN
			INSERT INTO dbo.BANGGIATRUCTUYEN
			        ( MaCK ,
			          GiaMua3 ,
			          SoLuongMua3 
			        )
			SELECT MaCK, GiaDat, SL FROM #TEMP_MUA WHERE RN = 3
		END
	END
END
ELSE
BEGIN
	SELECT TOP 3 MaCK, GiaDat, SUM(ISNULL(SOLUONG, 0)) AS SL, ROW_NUMBER() OVER(ORDER BY GiaDat) AS RN INTO #TEMP_BAN
	FROM dbo.LENHDAT 
	WHERE MaCK = @MACP 
		AND LoaiGiaoDich = @LOAI_GD
		AND CAST(NgayDat as DATE) = CAST(getdate() as DATE)
		and (TrangThai = 'KP' or TrangThai = 'CK')
	GROUP BY MaCK, GiaDat
	HAVING SUM(ISNULL(SOLUONG, 0)) > 0
	UPDATE dbo.BANGGIATRUCTUYEN
	SET GiaBan1 = NULL, SoLuongBan1 = NULL, GiaBan2 = NULL, SoLuongBan2 = NULL, GiaBan3 = NULL, SoLuongBan3 = NULL
	WHERE MaCK = @MACP

	-- GIA 1
	IF EXISTS (SELECT * FROM #TEMP_BAN WHERE MaCK = @MACP AND RN = 1)
	BEGIN
		IF EXISTS (SELECT * FROM dbo.BANGGIATRUCTUYEN WHERE MaCK = @MACP)
		BEGIN
			UPDATE dbo.BANGGIATRUCTUYEN
			SET GiaBan1 = (SELECT GiaDat FROM #TEMP_BAN WHERE RN = 1),
				SoLuongBan1 = (SELECT SL FROM #TEMP_BAN WHERE RN = 1)
			WHERE MaCK = @MACP
		END
		ELSE
		BEGIN
			INSERT INTO dbo.BANGGIATRUCTUYEN
			        ( MaCK ,
			          GiaBan1 ,
			          SoLuongBan1 
			        )
			SELECT MaCK, GiaDat, SL FROM #TEMP_BAN WHERE RN = 1
		END
	END

	-- GIA 2
	IF EXISTS (SELECT * FROM #TEMP_BAN WHERE MACK = @MACP AND RN = 2)
	BEGIN
		IF EXISTS (SELECT * FROM dbo.BANGGIATRUCTUYEN WHERE MACK = @MACP)
		BEGIN
			UPDATE dbo.BANGGIATRUCTUYEN
			SET GiaBan2 = (SELECT GiaDat FROM #TEMP_BAN WHERE RN = 2),
				SoLuongBan2 = (SELECT SL FROM #TEMP_BAN WHERE RN = 2)
			WHERE MaCK = @MACP
		END
		ELSE
		BEGIN
			INSERT INTO dbo.BANGGIATRUCTUYEN
			        ( MaCK ,
			          GiaBan2 ,
			          SoLuongBan2 
			        )
			SELECT MaCK, GiaDat, SL FROM #TEMP_BAN WHERE RN = 2
		END
	END

	-- GIA 3
	IF EXISTS (SELECT * FROM #TEMP_BAN WHERE MaCK = @MACP AND RN = 3)
	BEGIN
		IF EXISTS (SELECT * FROM dbo.BANGGIATRUCTUYEN WHERE MaCK = @MACP)
		BEGIN
			UPDATE dbo.BANGGIATRUCTUYEN
			SET GiaBan3 = (SELECT GiaDat FROM #TEMP_BAN WHERE RN = 3),
				SoLuongBan3 = (SELECT SL FROM #TEMP_BAN WHERE RN = 3)
			WHERE MaCK = @MACP
		END
		ELSE
		BEGIN
			INSERT INTO dbo.BANGGIATRUCTUYEN
			        ( MaCK ,
			          GiaBan3 ,
			          SoLuongBan3 
			        )
			SELECT MaCK, GiaDat, SL FROM #TEMP_BAN WHERE RN = 3
		END
	END
END
END