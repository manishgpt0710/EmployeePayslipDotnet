using EmployeePayslip.Data;
using EmployeePayslip.Services;
using EmployeePayslip.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Moq;

namespace EmployeePayslip.UnitTests.TestHelpers;
public class MockHelpers
{
    public static IEmployeePayslipManager CreateEmployeePayslipManagerObject()
    {
        var cache = new MemoryCache(new MemoryCacheOptions());
        var _memoryCacheWrapperMock = new MemoryCacheWrapper(cache);
        var _employeePayslipManager = new EmployeePayslipManager(_memoryCacheWrapperMock);
        return _employeePayslipManager;
    }

    public static IEmployeePayslipService CreateEmployeePayslipServiceObject()
    {
        var _employeePayslipManager = CreateEmployeePayslipManagerObject();
        var _incomeTaxServiceMock = new Mock<IIncomeTaxService>();
        var _configuration = CreateMockConfiguration();

        var _employeePayslipService = new EmployeePayslipService(
            _employeePayslipManager,
            _incomeTaxServiceMock.Object,
            _configuration.Object
        );
        return _employeePayslipService;
    }

    public static Mock<IConfiguration> CreateMockConfiguration()
    {
        // Create a ConfigurationBuilder
        var configurationBuilder = new ConfigurationBuilder();

        // Add the desired key-value pair to the configuration
        configurationBuilder.AddInMemoryCollection(new[]
            {
                new KeyValuePair<string, string?>("EmployeePayslipDataCacheKey", "TestEmployeeDataKey")
            });

        // Build the IConfiguration instance
        var configuration = configurationBuilder.Build();
        var mockConfiguration = new Mock<IConfiguration>();
        mockConfiguration.Setup(x => x["EmployeePayslipDataCacheKey"]).Returns(configuration["EmployeePayslipDataCacheKey"]);
        return mockConfiguration;
    }

}