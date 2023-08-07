using System.Collections.Generic;
using System.Linq;
using EmployeePayslip.Data;
using EmployeePayslip.Models;
using EmployeePayslip.UnitTests.TestHelpers;
using EmployeePayslip.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace EmployeePayslip.UnitTests.Data
{
    public class EmployeePayslipManagerTests
    {
        private readonly IEmployeePayslipManager _employeePayslipManager;

        private const string DATA_CACHE_KEY = "TestEmployeeDataKey";
        private const string NO_DATA_CACHE_KEY = "NoDataKey";
        public EmployeePayslipManagerTests()
        {
            _employeePayslipManager = MockHelpers.CreateEmployeePayslipManagerObject();
        }

        [Fact]
        public void GetData_ShouldReturnCachedData_IfAvailable()
        {
            // Arrange
            var expectedData = new EmployeePayslipResponse
            {
                Name = "Alex Wong",
                PayPeriod = "01 March - 31 March",
                GrossIncome = 1000,
                IncomeTax = 105,
                NetIncome = 895,
                Super = 100
            };
            _employeePayslipManager.SetData(DATA_CACHE_KEY, expectedData);

            // Act
            var result = _employeePayslipManager.GetData(DATA_CACHE_KEY);

            // Assert
            Assert.Same(expectedData, result);
        }

        [Fact]
        public void GetData_ShouldReturnEmptyList_IfDataNotCached()
        {
            // Arrange

            // Act
            var result = _employeePayslipManager.GetData(NO_DATA_CACHE_KEY);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void SetData_ShouldAddResponseToListAndCacheIt()
        {
            // Arrange
            var responseToAdd = new EmployeePayslipResponse {
                Name = "Alex Wong",
                PayPeriod = "01 March - 31 March",
                GrossIncome = 1000,
                IncomeTax = 105,
                NetIncome = 895,
                Super = 100
            };

            // Act
            var result = _employeePayslipManager.SetData(DATA_CACHE_KEY, responseToAdd);

            // Assert
            var resultList = result.ToList();
            Assert.Equal(2, resultList.Count); // The existing response and the one added
            Assert.Contains(responseToAdd, resultList); // The newly added response is present in the result
        }
    }
}