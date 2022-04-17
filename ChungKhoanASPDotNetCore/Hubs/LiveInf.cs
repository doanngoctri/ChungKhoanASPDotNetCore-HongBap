using Microsoft.AspNetCore.SignalR;
namespace ChungKhoanASPDotNetCore.Hubs
{
    public class LiveInf: Hub
    {
        private readonly IHubContext<LiveInf> _hub;

        public LiveInf(IHubContext<LiveInf> hub)
        {
            _hub = hub;
        }
        public void SendToClient(string message)
        {
            _hub.Clients.All.SendAsync(message);
        }
    }
}
