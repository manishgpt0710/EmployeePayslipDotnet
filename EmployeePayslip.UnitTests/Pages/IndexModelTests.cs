using EmployeePayslip.Models;
using EmployeePayslip.Pages;
using EmployeePayslip.Services;
using EmployeePayslip.UnitTests.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Moq;

namespace EmployeePayslip.UnitTests.Pages
{
    public class IndexModelTests
    {
        private readonly IndexModel _indexModel;
        private readonly Mock<IEmployeePayslipService> _employeePayslipServiceMock;
        private readonly Mock<ILogger<IndexModel>> _loggerMock;

        public IndexModelTests()
        {
            _employeePayslipServiceMock = new Mock<IEmployeePayslipService>();
            _loggerMock = new Mock<ILogger<IndexModel>>();
            _indexModel = new IndexModel(
                _employeePayslipServiceMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public void OnGet_ShouldPopulate_PeriodMonths()
        {
            // Arrange
            var expectedMonthNamesCount = 12;

            // Act
            _indexModel.OnGet();

            // Assert
            Assert.NotNull(IndexModel.PeriodMonths);
            Assert.Equal(expectedMonthNamesCount, IndexModel.PeriodMonths.Count);
            Assert.True(IndexModel.PeriodMonths[0].Selected);
        }

        [Fact]
        public void OnGet_ShouldCall_GetEmployeePayslipData()
        {
            // Arrange

            // Act
            _indexModel.OnGet();

            // Assert
            _employeePayslipServiceMock.Verify(mock => mock.GetEmployeePayslipData(), Times.Once);
        }

        [Fact]
        public void OnPostAsync_WithInvalidModelState_ShouldReturnPage()
        {
            // Arrange
            _indexModel.ModelState.AddModelError("Property", "Error Message");

            // Act
            var result = _indexModel.OnPostAsync();

            // Assert
            Assert.IsType<PageResult>(result);
        }

        [Fact]
        public void OnPostAsync_ShouldRedirectToPage()
        {
            // Arrange
            var input = new EmployeePayslipModel
            {
                FirstName = "Alex",
                LastName = "Wong",
                Period = 3,
                AnnualSalary = 12000,
                SuperRate = 10
            };

            // Act
            _employeePayslipServiceMock.Setup(m => m.PostEmployeePayslipData(It.IsAny<EmployeePayslipModel>()))
                .Returns(GetEmployeePayslipResponse());
            var result = _indexModel.OnPostAsync();

            // Assert
            var redirectResult = Assert.IsType<RedirectToPageResult>(result);
            Assert.Equal("Index", redirectResult.PageName);
        }

        private static List<EmployeePayslipResponse> GetEmployeePayslipResponse()
        {
            return new List<EmployeePayslipResponse>{
                new EmployeePayslipResponse{
                    Name = "Alex Wong",
                    PayPeriod = "01 March - 31 March",
                    GrossIncome = 1000,
                    IncomeTax = 105,
                    NetIncome = 895,
                    Super = 100
                }
            };
        }
    }


}
