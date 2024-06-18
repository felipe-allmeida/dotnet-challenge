using BikeRental.API.FunctionalTests.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace BikeRental.API.FunctionalTests.Extensions
{
    public static class WebApplicationFactoryExtensions
    {
        public static HttpClient CreateClientWithMockedAuthentication<T>(this WebApplicationFactory<T> src) where T : class
        {
            var client = src.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services
                        .AddAuthentication(x =>
                        {
                            x.DefaultAuthenticateScheme = TestAuthHandler.AuthenticationScheme;
                            x.DefaultScheme = TestAuthHandler.AuthenticationScheme;
                        })
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.AuthenticationScheme, options => { });
                });
            }).CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme: TestAuthHandler.AuthenticationScheme);

            return client;
        }
    }

}
