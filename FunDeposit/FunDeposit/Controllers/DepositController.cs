using AutoMapper;
using FunDeposit.Services;
using FunDeposit.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FunDeposit.Controllers
{
    public class DepositController : Controller
    {
        private readonly IDepositService _depositService;
        private readonly IMapper _mapper;

        public DepositController(IDepositService depositService, IMapper mapper)
        {
            _depositService = depositService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var deposits = _depositService.Deposits;

            var vm = new DepositsVM()
            {
                Deposits = _mapper.Map<List<DepositVM>>(deposits)
            };

            return View(vm);
        }
    }
}