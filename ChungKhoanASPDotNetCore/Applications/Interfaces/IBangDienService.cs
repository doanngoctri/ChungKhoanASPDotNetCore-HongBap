using ChungKhoanASPDotNetCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ChungKhoanASPDotNetCore.Applications.Interfaces
{
    public interface IBangDienService
    {
        List<BangGiaTrucTuyen> Get();
    }
}
