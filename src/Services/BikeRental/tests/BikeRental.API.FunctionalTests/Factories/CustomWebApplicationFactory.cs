using BikeRental.API.FunctionalTests.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace BikeRental.API.FunctionalTests.Factories
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public Dictionary<string, object> Data { get; set; }

        public CustomWebApplicationFactory() : base()
        {
            Data = new Dictionary<string, object>();
        }
    }

    public class MockedAuthWebApplicationFactory : CustomWebApplicationFactory
    {
        public Dictionary<string, object> Data { get; set; }

        public MockedAuthWebApplicationFactory() : base()
        {
            Data = new Dictionary<string, object>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
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
        }
    }

}
