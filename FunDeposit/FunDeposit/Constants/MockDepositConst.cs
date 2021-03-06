﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunDeposit.Constants
{
    public class MockDepositConst
    {
        public const int InitialNumberOfDeposits = 50;
        public const double MinTotalMaturityAmount = 7e7;
        public const double MaxTotalMaturityAmount = 1e8;
        public const double LimitTotalMaturityAmountForRemove = 5e7;
        public const double LimitTotalMaturityAmountForAdd = 12e7;
        public const double MinPrincipalForAddAndRemove = 3e6;
        public const double MaxPrincipalForAddAndRemove = 5e6;
    }
}
