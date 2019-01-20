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

        public bool AddDeposit()
        {
            var totalMaturityAmount = Deposits.Sum(d => d.MaturityAmount);

            if(totalMaturityAmount >= MockDepositConst.LimitTotalMaturityAmountForAdd)
            {
                return false;
            }

            var deposit = GenerateDepositByPrincipal(MockDepositConst.MinPrincipalForAddAndRemove, MockDepositConst.MaxPrincipalForAddAndRemove);

            Deposits.Add(deposit);

            return true;
        }

        public bool RemoveDeposit()
        {
            var totalMaturityAmount = Deposits.Sum(d => d.MaturityAmount);

            if (totalMaturityAmount <= MockDepositConst.LimitTotalMaturityAmountForRemove)
            {
                return false;
            }

            Deposits.RemoveAt(0);

            return true;
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
                var deposit = GenerateDepositByMaturityAmount(minEachMaturityAmount, maxEachMaturityAmount);

                depositList.Add(deposit);
            }

            return depositList;
        }

        private DepositModel GenerateDepositByPrincipal(double minPrincipal, double maxPrincipal)
        {
            var randomPrincipal = GetRandomDouble(minPrincipal, maxPrincipal);

            // Get random rate with associated term
            (var term, var rate) = GetRandomTermAndRate();

            var deposit = new DepositModel(randomPrincipal)
            {
                Term = term,
                InterestRate = rate,
                StartDate = GetRandomDateTime(),
            };

            return deposit;
        }

        private DepositModel GenerateDepositByMaturityAmount(double minEachMaturityAmount, double maxEachMaturityAmount)
        {
            var EachMaturityAmount = GetRandomDouble(minEachMaturityAmount, maxEachMaturityAmount);

            // Get random rate with associated term
            (var term, var rate) = GetRandomTermAndRate();

            // Calculate principal
            var principal = EachMaturityAmount / Math.Pow((1 + rate), term);

            var deposit = new DepositModel(principal)
            {
                Term = term,
                InterestRate = rate,
                StartDate = GetRandomDateTime(),
            };

            return deposit;
        }

        private (int term, double rate) GetRandomTermAndRate()
        {
            var randomTermAndRateIndex = random.Next(TermDepositConst.TermAndRate.Count);

            return (TermDepositConst.TermAndRate[randomTermAndRateIndex].Term, TermDepositConst.TermAndRate[randomTermAndRateIndex].InterestRate);
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
