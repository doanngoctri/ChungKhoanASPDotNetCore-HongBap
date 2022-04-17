namespace ChungKhoanASPDotNetCore.Models.RequestModels
{
    public class LenhDatRequest
    {
        public string MaCK { get; set; }
        public bool LoaiGiaoDich { get; set; }
        public string LoaiLenh { get; set; } = "LO";
        public int SoLuong { get; set; }
        public double GiaDat { get; set; }
    }
}
