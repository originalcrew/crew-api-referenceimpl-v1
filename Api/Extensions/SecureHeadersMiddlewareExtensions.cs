using System.Collections.Generic;
using OwaspHeaders.Core.Enums;
using OwaspHeaders.Core.Extensions;
using OwaspHeaders.Core.Models;

namespace Crew.Api.ReferenceImpl.V1.Extensions
{
    public static class SecureHeadersMiddlewareExtensions
    {
        public static SecureHeadersMiddlewareConfiguration BuildCrewConfiguration()
        {
            SecureHeadersMiddlewareConfiguration config = OwaspHeaders.Core.Extensions.SecureHeadersMiddlewareExtensions.BuildDefaultConfiguration();

            config.UseXSSProtection(); // largely unnecessary in modern browsers, so it is not a part of the default config above. https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection

            /* must whitelist each of the inline <script> usages :-( - but the Chrome DevTools console will give you each of these hashes :-) */
            config.ContentSecurityPolicyConfiguration.ScriptSrc.AddRange(
                new List<ContentSecurityPolicyElement>
                {
                    new ContentSecurityPolicyElement
                    {
                        CommandType = CspCommandType.Directive,
                        DirectiveOrUri = "sha256-Tui7QoFlnLXkJCSl1/JvEZdIXTmBttnWNxzJpXomQjg=" // /swagger/index.html:36
                    },
                    new ContentSecurityPolicyElement
                    {
                        CommandType = CspCommandType.Directive,
                        DirectiveOrUri = "sha256-gq6S6/3Ezewo+pui4kNYr482Vrh9LGBv0hNW3GQnjuU=" // /swagger/index.html:45
                    }
                });

            return config;
        }
    }
}