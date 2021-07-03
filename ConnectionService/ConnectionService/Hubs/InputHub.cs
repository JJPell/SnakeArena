using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ConnectionService.Hubs
{
    public class InputHub : Hub
    {
        public async Task Input(string message)
        {
            //do something here

        }
    }
}
