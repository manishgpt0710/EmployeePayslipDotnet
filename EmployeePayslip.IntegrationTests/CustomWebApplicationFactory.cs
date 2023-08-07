using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace EmployeePayslip.IntegrationTests;

[CollectionDefinition("IntegrationTests")]
public class CustomWebApplicationFactoryCollection : ICollectionFixture<CustomWebApplicationFactory>
{

}

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((ctx, config) =>
        {
            IEnumerable<KeyValuePair<string, string?>>? initialData = new[]
            {
                new KeyValuePair<string, string?>("EmployeePayslipDataCacheKey", "IntegrationTestEmployeeDataKey")
            };
            config.AddInMemoryCollection(initialData);
        });

        builder.ConfigureServices(PreStartupConfigureServices);
        builder.ConfigureTestServices(PostStartupConfigureServices);
    }

    private void PreStartupConfigureServices(IServiceCollection services)
    {
        // Use this method to resolve dependencies for configure services
    }

    private void PostStartupConfigureServices(IServiceCollection services)
    {
        // Use this method to register dependencies for configure services
    }

    protected override void ConfigureClient(HttpClient client)
    {
        // Configure client as per requirement
    }
}
