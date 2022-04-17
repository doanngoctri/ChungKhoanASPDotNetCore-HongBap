using System;
namespace ChungKhoanASPDotNetCore.Entities
{
    public class LenhKhop
    {
        public int Id { get; set; }
        public DateTime NgayKhop { get; set; }
        public int SoLuongKhop { get; set; }
        public double GiaKhop { get; set; }
        public int IdLenhDat { get; set; }
        public LenhDat LenhDat { get; set; }
    }
}
