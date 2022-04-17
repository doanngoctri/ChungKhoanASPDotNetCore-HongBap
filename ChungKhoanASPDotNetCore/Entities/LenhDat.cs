using System;
using System.Collections.Generic;
namespace ChungKhoanASPDotNetCore.Entities
{
    public class LenhDat
    {
        public int Id { get; set; }
        public string MaCK { get; set; }
        public DateTime NgayDat { get; set; }
        public bool LoaiGiaoDich { get; set; }
        public string LoaiLenh { get; set; }
        public int SoLuong { get; set; }
        public double GiaDat { get; set; }
        public string TrangThai { get; set; }
        public ICollection<LenhKhop> LenhKhops {get;set;}
    }
}
