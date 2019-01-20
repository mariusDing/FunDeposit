using FunDeposit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunDeposit.Services
{
    public interface IDepositService
    {
        List<DepositModel> Deposits { get; set; }

        bool AddDeposit();

        bool RemoveDeposit();
    }
}
