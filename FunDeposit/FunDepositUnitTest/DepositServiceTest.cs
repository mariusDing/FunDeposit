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
            var depositeService = new DepositService();
            var expectedMinTotalMaturityAmount = 7e7;
            var expectedMaxTotalMaturityAmount = 1e8;
            
            // Action
            var actualTotalMaturityAmount = depositeService.Deposits.Sum(d => d.MaturityAmount);

            // Assert
            Assert.InRange(actualTotalMaturityAmount, expectedMinTotalMaturityAmount, expectedMaxTotalMaturityAmount);
        }
    }
}
