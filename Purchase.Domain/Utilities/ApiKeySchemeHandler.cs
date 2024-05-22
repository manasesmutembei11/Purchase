using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Purchase.Domain.Models.UserEntities;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Purchase.Domain.Utilities
{
    public class ApiKeySchemeHandler : AuthenticationHandler<ApiKeySchemeOptions>
    {
        private readonly IConfiguration _configuration;

        public ApiKeySchemeHandler(
            IConfiguration configuration,
            IOptionsMonitor<ApiKeySchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder
            ) : base(options, logger, encoder)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Options.HeaderName))
            {
                return AuthenticateResult.Fail("Header Not Found.");
            }
            var apiKey = Request.Headers[Options.HeaderName];
            if (!await IsApiKeyValid(apiKey))
            {
                return AuthenticateResult.Fail("Invalid Api Key.");
            }
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, $"{apiKey}"),

                new Claim(CustomClaimTypes.Permission, Permissions.Invoicing.View),
                new Claim(CustomClaimTypes.Permission, Permissions.Invoicing.Process),


            };
            var identiy = new ClaimsIdentity(claims, nameof(ApiKeySchemeHandler));
            var principal = new ClaimsPrincipal(identiy);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        private Task<bool> IsApiKeyValid(StringValues apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return Task.FromResult(false);
            }
            string actualApiKey = _configuration.GetValue<string>("ApiKey");
            return Task.FromResult(apiKey == actualApiKey);
        }
    }
}
