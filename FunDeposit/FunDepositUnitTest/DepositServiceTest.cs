using FunDeposit.Services;
using System;
using System.Linq;
using Xunit;

namespace FunDepositUnitTest
{
    public class DepositServiceTest
    {
        [Fact]
        public void Should_TotalMaturityAmountOfGenertatedDeposits_LessOrEqual100M_GreatOrEqual70M()
        {
            // Arrange
            var depositService = new DepositService();

            var expectedMinTotalMaturityAmount = 7e7;

            var expectedMaxTotalMaturityAmount = 1e8;
            
            // Action
            var actualTotalMaturityAmount = depositService.Deposits.Sum(d => d.MaturityAmount);

            // Assert
            Assert.InRange(actualTotalMaturityAmount, expectedMinTotalMaturityAmount, expectedMaxTotalMaturityAmount);
        }

        [Fact]
        public void Should_AddDeposit_AddNewDeposit_And_PrincipleAmount_GreaterOrEqual3M_LessOrEqual5M()
        {
            // Arrange
            var expectedMinTotalMaturityAmount = 3e6;

            var expectedMaxTotalMaturityAmount = 5e6;

            var depositService = new DepositService();

            var deposits = depositService.Deposits;

            var beforeCount = depositService.Deposits.Count;

            // Action
            var isSuccess = depositService.AddDeposit();

            var newDeposit = deposits.Last();

            // Assert
            Assert.Equal(beforeCount, depositService.Deposits.Count - 1);
            Assert.InRange(newDeposit.Principal, expectedMinTotalMaturityAmount, expectedMaxTotalMaturityAmount);
        }

        [Fact]
        public void Should_RemoveDeposit_RmoveOneDeposit()
        {
            // Arrange
            var depositService = new DepositService();

            var deposits = depositService.Deposits;

            var beforeCount = depositService.Deposits.Count;

            // Action
            var isSuccess = depositService.RemoveDeposit();

            // Assert
            Assert.Equal(beforeCount, depositService.Deposits.Count + 1);
        }

        [Fact]
        public void Should_AddDeposit_ReachTheLimit_AtTotalMaturityAmount_GreatOrEqual120M()
        {
            // Arrange
            var MaxLimitForTotalMaturityAmount = 12e7;

            var depositService = new DepositService();

            bool isSuccess = true;

            // Action
            while (isSuccess)
            {
                isSuccess = depositService.AddDeposit();
            }

            var totalMaturityAmount = depositService.Deposits.Sum(d => d.MaturityAmount);

            // Assert
            Assert.True(totalMaturityAmount >= MaxLimitForTotalMaturityAmount);
        }

        [Fact]
        public void Should_Remove_ReachTheLimit_AtTotalMaturityAmount_LessOrEqual50M()
        {
            // Arrange
            var MinLimitForTotalMaturityAmount = 5e7;

            var depositService = new DepositService();

            bool isSuccess = true;

            // Action
            while (isSuccess)
            {
                isSuccess = depositService.RemoveDeposit();
            }

            var totalMaturityAmount = depositService.Deposits.Sum(d => d.MaturityAmount);

            // Assert
            Assert.True(totalMaturityAmount <= MinLimitForTotalMaturityAmount);
        }
    }
}
