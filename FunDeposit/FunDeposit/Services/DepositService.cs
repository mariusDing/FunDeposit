using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FunDeposit.Constants;
using FunDeposit.Models;

namespace FunDeposit.Services
{
    public class DepositService : IDepositService
    {
        private static readonly Random random = new Random();

        public List<DepositModel> Deposits { get; set; }

        // Service will be injected as Singleton
        public DepositService()
        {
            if (Deposits == null)
            {
                Deposits = GenerateMockDeposits(MockDepositConst.InitialNumberOfDeposits,
                                                MockDepositConst.MinTotalMaturityAmount,
                                                MockDepositConst.MaxTotalMaturityAmount);
            }
        }

        private List<DepositModel> GenerateMockDeposits(int numberOfRecords, double minTotalMaturityAmount, double maxTotalMaturityAmount)
        {
            var depositList = new List<DepositModel>();

            // Get range for maturity amount of each deposit 
            var minEachMaturityAmount = minTotalMaturityAmount / numberOfRecords;

            var maxEachMaturityAmount = maxTotalMaturityAmount / numberOfRecords;

            // Generate and add number of records to the list 
            for (int i = 0; i < numberOfRecords; i++)
            {
                var deposit = GenerateDeposit(minEachMaturityAmount, maxEachMaturityAmount);

                depositList.Add(deposit);
            }

            return depositList;
        }

        private DepositModel GenerateDeposit(double minEachMaturityAmount, double maxEachMaturityAmount)
        {
            var EachMaturityAmount = GetRandomDouble(minEachMaturityAmount, maxEachMaturityAmount);

            // Get random rate with associated term
            var randomTermAndRateIndex = random.Next(TermDepositConst.TermAndRate.Count);

            var term = TermDepositConst.TermAndRate[randomTermAndRateIndex].Term;

            var rate = TermDepositConst.TermAndRate[randomTermAndRateIndex].InterestRate;

            // Calculate principal
            var principal = EachMaturityAmount / Math.Pow((1 + rate), term);

            var deposite = new DepositModel(principal)
            {
                Term = term,
                InterestRate = rate,
                StartDate = GetRandomDateTime(),
            };

            return deposite;
        }

        private double GetRandomDouble(double min, double max)
        {
            var next = random.NextDouble();

            var randomTotal = min + (next * (max - min));

            return randomTotal;
        }

        private DateTime GetRandomDateTime()
        {
            return DateTime.Now.AddDays(random.Next(1000));
        }
    }
}
