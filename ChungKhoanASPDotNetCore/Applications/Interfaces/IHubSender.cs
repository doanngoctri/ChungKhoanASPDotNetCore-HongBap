using ChungKhoanASPDotNetCore.Entities;

namespace ChungKhoanASPDotNetCore.Applications.Interfaces
{
    public interface IHubSender
    {
        void SendChange(BangGiaTrucTuyen item);
    }
}
