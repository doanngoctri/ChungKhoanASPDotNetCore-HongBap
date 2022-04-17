using System;
namespace ChungKhoanASPDotNetCore.Models.ViewModels
{
    public class LenhKhopViewModel
    {
        public int Id { get; set; }
        public DateTime NgayKhop { get; set; }
        public int SoLuongKhop { get; set; }
        public double GiaKhop { get; set; }
    }
}
