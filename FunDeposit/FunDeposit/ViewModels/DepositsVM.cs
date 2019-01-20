using System;
using System.Collections.Generic;
using System.Linq;

namespace FunDeposit.ViewModels
{
    public class DepositsVM
    {
        public List<DepositVM> Deposits = new List<DepositVM>();

        public double TotalMaturityAmount => Deposits.Sum(d => d.MaturityAmount);
    }

    public class DepositVM
    {
        public double Principal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double InterestRate { get; set; }
        public int Term { get; set; }
        public double MaturityAmount { get; set; }
    }
}
