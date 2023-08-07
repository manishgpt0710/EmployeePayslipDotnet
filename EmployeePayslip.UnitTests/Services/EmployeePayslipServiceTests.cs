using EmployeePayslip.Models;
using EmployeePayslip.Services;
using EmployeePayslip.UnitTests.TestHelpers;
using Moq;

namespace EmployeePayslip.UnitTests.Services
{
    public class EmployeePayslipServiceTests
    {
        private readonly IEmployeePayslipService _employeePayslipService;
        private readonly Mock<IIncomeTaxService> _incomeTaxServiceMock;
        public EmployeePayslipServiceTests()
        {
            _incomeTaxServiceMock = new Mock<IIncomeTaxService>();
            _employeePayslipService = MockHelpers.CreateEmployeePayslipServiceObject();
        }

        [Fact]
        public void GetEmployeePayslipData_ShouldCall_GetData()
        {
            // Arrange
            var expectedResponse = new List<EmployeePayslipResponse>();

            // Act
            var result = _employeePayslipService.GetEmployeePayslipData().ToList();

            // Assert
            Assert.Equal(expectedResponse, result);
        }

        [Fact]
        public void PostEmployeePayslipData_ShouldCalculatePayslipResponse()
        {
            // Arrange
            var employeePayslipModel = new EmployeePayslipModel
            {
                FirstName = "John",
                LastName = "Doe",
                AnnualSalary = 60000,
                Period = 7, // July
                SuperRate = 9
            };

            var expectedGrossIncome = 5000;
            var expectedIncomeTax = 922;
            var expectedNetIncome = expectedGrossIncome - expectedIncomeTax;
            var expectedSuper = 450;

            _incomeTaxServiceMock.Setup(service => service.GetIncomeTax(employeePayslipModel.AnnualSalary))
                .Returns(expectedIncomeTax);

            // Act
            var result = _employeePayslipService.PostEmployeePayslipData(employeePayslipModel);

            // Assert
            var payslipResponse = Assert.Single(result);
            Assert.Equal($"{employeePayslipModel.FirstName} {employeePayslipModel.LastName}", payslipResponse.Name);
            Assert.Equal("1 July - 31 July", payslipResponse.PayPeriod);
            Assert.Equal(expectedGrossIncome, payslipResponse.GrossIncome);
            Assert.Equal(expectedIncomeTax, payslipResponse.IncomeTax);
            Assert.Equal(expectedNetIncome, payslipResponse.NetIncome);
            Assert.Equal(expectedSuper, payslipResponse.Super);
        }
    }
}