using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FunDeposit
{
    public class DepositHub : Hub
    {
        public async Task SendDeposits(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveDeposits", user, message);
        }
    }
}
