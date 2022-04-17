using ChungKhoanASPDotNetCore.Applications.Interfaces;
using ChungKhoanASPDotNetCore.Entities;
using ChungKhoanASPDotNetCore.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace ChungKhoanASPDotNetCore.Applications.Services
{
    public class HubSender: IHubSender
    {
        private IHubContext<LiveInf> _lf;

        public HubSender(IHubContext<LiveInf> lf)
        {
            _lf = lf;
        }
        public void SendChange(BangGiaTrucTuyen item)
        {
            var json = JsonSerializer.Serialize(item);

            _lf.Clients.All.SendAsync("message", json);
        }
    }
}
