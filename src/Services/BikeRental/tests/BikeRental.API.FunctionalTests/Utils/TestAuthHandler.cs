using BikeRental.API.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BikeRental.API.FunctionalTests.Utils
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string AuthenticationScheme = "Test";
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder)
            : base(options, logger, encoder)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, "7881c538-0e31-4141-b3ae-6659f930bb85"),
                new Claim(ClaimTypes.NameIdentifier, "7881c538-0e31-4141-b3ae-6659f930bb85"),
                new Claim(JwtRegisteredClaimNames.Email, "admin@bikerental.com"),

                new Claim(ClaimTypes.Role, Roles.Admin),
                new Claim(Claims.Global, ClaimValues.Read),
                new Claim(Claims.Global, ClaimValues.Write),

                new Claim(ClaimTypes.Role, Roles.DeliveryRider),
                new Claim(Claims.DeliveryRider, ClaimValues.Read),
                new Claim(Claims.DeliveryRider, ClaimValues.Write),
            };

            var identity = new ClaimsIdentity(claims, AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, AuthenticationScheme);

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }

}
