using ChungKhoanASPDotNetCore.Models.RequestModels;
using ChungKhoanASPDotNetCore.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChungKhoanASPDotNetCore.Applications.Interfaces
{
    public interface ILenhDatService
    {
        Task<List<LenhDatViewModel>> GetAll();
        Task<LenhDatViewModel> Get(int id);
        Task<bool> Post(LenhDatRequest request);
    }
}
