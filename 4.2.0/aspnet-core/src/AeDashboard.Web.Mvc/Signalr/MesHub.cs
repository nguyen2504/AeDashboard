using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AeDashboard.Web.Signalr
{
    public class MesHub:Hub
    {
        public async Task SendMessage(int id, string table,string action)
        {
            await Clients.All.SendAsync("ReceiveMessage", id, table,action);
        }
    }
}
