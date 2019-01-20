using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunDeposit.Constants
{
    public class TermDepositConst
    {
        // Predefine rates and fee for a term deposit
        // 2.20% p.a. for 1 year
        // 2.23% p.a. for 3 years
        // 2.65% p.a. fro 5 years 
        public static readonly List<(int Term, double InterestRate)> TermAndRate = new List<(int Term, double InterestRate)> {(1, 0.0220), (2, 0.0230) , (5, 0.0265)};
    }
}
