using EmployeePayslip.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace EmployeePayslip.UnitTests.Mocks;
internal abstract class MockParamsForUnitTest<T>
{
    protected MockParamsForUnitTest()
    {
        MockLogger = new Mock<ILogger<T>>();
    }

    public Mock<ILogger<T>> MockLogger { get; }

}