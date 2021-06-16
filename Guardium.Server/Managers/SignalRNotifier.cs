using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Guardium.Server.Model;

namespace Guardium.Server
{
    public class SignalRNotifier : INotifier
    {
        private readonly IHubContext<PaintSignalRHub> _hub;

        public SignalRNotifier(IHubContext<PaintSignalRHub> hub)
        {
            this._hub = hub;
        }
        public async Task NotifyClientsOnAdd(string uuid, ElementContent elementContent)
        {
            await _hub.Clients.All.SendAsync("Refresh", uuid, "Add", elementContent);
        }
        public async Task NotifyClientsOnDelete(string uuid, string elementIdentifier)
        {
            await _hub.Clients.All.SendAsync("Refresh", uuid, "Remove", elementIdentifier);
        }
    }
}
