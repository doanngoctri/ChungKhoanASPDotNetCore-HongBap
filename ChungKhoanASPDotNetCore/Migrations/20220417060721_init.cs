using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChungKhoanASPDotNetCore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_History",
                columns: table => new
                {
                    FileName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Script = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__History", x => x.FileName);
                });

            migrationBuilder.CreateTable(
                name: "BangGiaTrucTuyen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCK = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GiaMua1 = table.Column<double>(type: "float", nullable: true),
                    SoLuongMua1 = table.Column<int>(type: "int", nullable: true),
                    GiaMua2 = table.Column<double>(type: "float", nullable: true),
                    SoLuongMua2 = table.Column<int>(type: "int", nullable: true),
                    GiaMua3 = table.Column<double>(type: "float", nullable: true),
                    SoLuongMua3 = table.Column<int>(type: "int", nullable: true),
                    GiaKhop = table.Column<double>(type: "float", nullable: true),
                    SoLuongKhop = table.Column<int>(type: "int", nullable: true),
                    GiaBan1 = table.Column<double>(type: "float", nullable: true),
                    SoLuongBan1 = table.Column<int>(type: "int", nullable: true),
                    GiaBan2 = table.Column<double>(type: "float", nullable: true),
                    SoLuongBan2 = table.Column<int>(type: "int", nullable: true),
                    GiaBan3 = table.Column<double>(type: "float", nullable: true),
                    SoLuongBan3 = table.Column<int>(type: "int", nullable: true),
                    TongSo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangGiaTrucTuyen", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LenhDat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaCK = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiGiaoDich = table.Column<bool>(type: "bit", nullable: false),
                    LoaiLenh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    GiaDat = table.Column<double>(type: "float", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LenhDat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LenhKhop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayKhop = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuongKhop = table.Column<int>(type: "int", nullable: false),
                    GiaKhop = table.Column<double>(type: "float", nullable: false),
                    IdLenhDat = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LenhKhop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LenhKhop_LenhDat_IdLenhDat",
                        column: x => x.IdLenhDat,
                        principalTable: "LenhDat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BangGiaTrucTuyen",
                columns: new[] { "Id", "GiaBan1", "GiaBan2", "GiaBan3", "GiaKhop", "GiaMua1", "GiaMua2", "GiaMua3", "MaCK", "SoLuongBan1", "SoLuongBan2", "SoLuongBan3", "SoLuongKhop", "SoLuongMua1", "SoLuongMua2", "SoLuongMua3", "TongSo" },
                values: new object[] { 1, null, null, null, null, null, null, null, "AAA", null, null, null, null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "BangGiaTrucTuyen",
                columns: new[] { "Id", "GiaBan1", "GiaBan2", "GiaBan3", "GiaKhop", "GiaMua1", "GiaMua2", "GiaMua3", "MaCK", "SoLuongBan1", "SoLuongBan2", "SoLuongBan3", "SoLuongKhop", "SoLuongMua1", "SoLuongMua2", "SoLuongMua3", "TongSo" },
                values: new object[] { 2, null, null, null, null, null, null, null, "BBB", null, null, null, null, null, null, null, null });

            migrationBuilder.InsertData(
                table: "BangGiaTrucTuyen",
                columns: new[] { "Id", "GiaBan1", "GiaBan2", "GiaBan3", "GiaKhop", "GiaMua1", "GiaMua2", "GiaMua3", "MaCK", "SoLuongBan1", "SoLuongBan2", "SoLuongBan3", "SoLuongKhop", "SoLuongMua1", "SoLuongMua2", "SoLuongMua3", "TongSo" },
                values: new object[] { 3, null, null, null, null, null, null, null, "CCC", null, null, null, null, null, null, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_BangGiaTrucTuyen_MaCK",
                table: "BangGiaTrucTuyen",
                column: "MaCK",
                unique: true,
                filter: "[MaCK] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LenhKhop_IdLenhDat",
                table: "LenhKhop",
                column: "IdLenhDat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_History");

            migrationBuilder.DropTable(
                name: "BangGiaTrucTuyen");

            migrationBuilder.DropTable(
                name: "LenhKhop");

            migrationBuilder.DropTable(
                name: "LenhDat");
        }
    }
}
