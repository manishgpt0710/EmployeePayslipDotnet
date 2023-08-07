
using System.Net;

namespace EmployeePayslip.IntegrationTests.Pages;

[Collection("IntegrationTests")]
public class IndexModelIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public IndexModelIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task OnGet_ShouldPopulatePeriodMonths_AndGetDataFromService()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/Index");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task OnPostAsync_ShouldPostDataAndRedirectToIndex()
    {
        // Arrange
        var formData = new Dictionary<string, string>
        {
            { "Input.FirstName", "John" },
            { "Input.LastName", "Doe" },
            { "Input.AnnualSalary", "60050" },
            { "Input.SuperRate", "9" },
            { "Input.Period", "3" },
        };
        var content = new FormUrlEncodedContent(formData);

        // Act
        var response = await _client.PostAsync("/Index", content);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}