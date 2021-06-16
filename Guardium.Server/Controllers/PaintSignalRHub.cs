using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Guardium.Server
{
    public class PaintSignalRHub : Hub
    {

        public PaintSignalRHub()
        {

        }

        //if I want to accept calls from client via singalr - I can add the relevant methods..
        //currrently - client calls to server using REST API.
        

        //public async Task PublishAdd(string pageUUID, ElementContent elementContent)
        //{
        //    await this.Clients.All.SendAsync("Refresh", pageUUID, "Add", elementContent);
        //}
 

        //public async Task PublishRemove(string pageUUID, string elementIdentifier)
        //{
        //    await this.Clients.All.SendAsync("Refresh", pageUUID, "Remove", elementIdentifier);
        //}
    }

}
