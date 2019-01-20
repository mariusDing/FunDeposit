using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FunDeposit.Services;
using FunDeposit.ViewModels;
using Microsoft.AspNetCore.SignalR;

namespace FunDeposit
{
    public class DepositHub : Hub
    {
        private readonly IDepositService _depositService;
        private readonly IMapper _mapper;

        public DepositHub(IDepositService depositService, IMapper mapper)
        {
            _depositService = depositService;
            _mapper = mapper;

        }
        public async Task BuyDeposit()
        {
            var isSuccess = _depositService.AddDeposit();

            var updatedVM = new DepositsVM()
            {
                Deposits = _mapper.Map<List<DepositVM>>(_depositService.Deposits)
            };

            await Clients.All.SendAsync("BuyDepositCallback", isSuccess, updatedVM);
        }

        public async Task RemoveDeposit()
        {
            var isSuccess = _depositService.RemoveDeposit();

            var updatedVM = new DepositsVM()
            {
                Deposits = _mapper.Map<List<DepositVM>>(_depositService.Deposits)
            };

            await Clients.All.SendAsync("RemoveDepositCallback", isSuccess, updatedVM);
        }
    }
}
