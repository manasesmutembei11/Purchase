using Microsoft.AspNetCore.Authentication;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Utilities
{
    public class ApiKeySchemeOptions : AuthenticationSchemeOptions
    {
        public const string Scheme = "ApiKeyScheme";
        public string HeaderName { get; set; } = HeaderNames.Authorization;
    }
}
