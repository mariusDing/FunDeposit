using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunDeposit.Models
{
    public class DepositModel
    {
        public DepositModel(double principal)
        {
            _principal = principal;
        }

        public double Principal => GetRoundingDouble(_principal);
        public DateTime StartDate { get; set; }
        public DateTime EndDate => StartDate.AddYears(Term).AddDays(-1);
        public double InterestRate { get; set; }
        public int Term { get; set; }
        public double MaturityAmount => GetRoundingDouble(_principal * Math.Pow((1 + InterestRate), Term)); //Maturity Value = P x (1 + Interest Rate)^Term

        private double _principal;
        private double GetRoundingDouble(double value)
        {
            return Math.Round(value, 2, MidpointRounding.ToEven); // Banker's rounding
        }
    }
}
