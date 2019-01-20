using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunDeposit.Services;
using Microsoft.AspNetCore.Mvc;

namespace FunDeposit.Controllers
{
    public class DepositController : Controller
    {
        private readonly IDepositService _depositService;

        public DepositController(IDepositService depositService)
        {
            _depositService = depositService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}