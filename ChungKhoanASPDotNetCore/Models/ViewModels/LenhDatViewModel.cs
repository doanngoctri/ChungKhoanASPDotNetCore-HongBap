using System;
using System.Collections.Generic;
namespace ChungKhoanASPDotNetCore.Models.ViewModels
{
    public class LenhDatViewModel
    {
        public int Id { get; set; }
        public string MaCK { get; set; }
        public DateTime NgayDat { get; set; }
        public string LoaiGiaoDich { get; set; }
        public string LoaiLenh { get; set; }
        public int SoLuong { get; set; }
        public double GiaDat { get; set; }
        public string TrangThai { get; set; }
        public ICollection<LenhKhopViewModel> LenhKhops { get; set; }
    }
}
