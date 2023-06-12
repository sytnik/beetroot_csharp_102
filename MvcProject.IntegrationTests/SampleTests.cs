using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MvcProject.IntegrationTests;

public class SampleTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public SampleTests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Theory]
    [InlineData("/")]
    [InlineData("/Home/PrivacyPage")]
    [InlineData("/Home/CreatePerson")]
    [InlineData("/Home/PersonsOrders")]
    public async Task GetSomePages(string url)
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        // if it's not 200-299, it will throw an exception
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
    }
    
    [Theory]
    [InlineData("/Home/ListPersons")]
    [InlineData("/Home/editperson/1")]
    public async Task GetRedirectIfNotAuth(string url)
    {
        // Arrange
        var client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        // Act
        var response = await client.GetAsync(url);
        // Assert
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Contains("/Home/Login?ReturnUrl=",
            response.Headers.Location?.OriginalString);
    }
    
    [Theory]
    [InlineData("/Home/ListPersons")]
    [InlineData("/Home/editperson/1")]
    public async Task Get_SecurePageIsReturnedForAnAuthenticatedUser(string url)
    {
        // Arrange
        var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                    services.AddAuthentication(defaultScheme: "TestScheme")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "TestScheme", _ => { }));
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(scheme: "TestScheme");
        //Act
        var response = await client.GetAsync(url);
        // Assert
        response.EnsureSuccessStatusCode();
        // if it's not 200-299, it will throw an exception
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
    }  
}

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "Test admin"),
            new(ClaimsIdentity.DefaultRoleClaimType, "Admin")
        };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "TestScheme");
        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}