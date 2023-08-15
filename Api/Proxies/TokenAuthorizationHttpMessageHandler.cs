using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Crew.Api.ReferenceImpl.V1.Proxies
{
    /*
     * inspired by Refit.AuthenticatedHttpClientHandler
     */
    public abstract class TokenAuthorizationHttpMessageHandler : DelegatingHandler
    {
        protected TokenAuthorizationHttpMessageHandler(ITokenService tokenService)
        {
            TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        public ITokenService TokenService { get; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            /* check if this request has an authorization header */
            AuthenticationHeaderValue authorizationHeader = request.Headers.Authorization;

            /* if we have an authorizationHeader, get the token & put it in the authorizationHeader, preserving the existing authorizationHeader scheme */
            if (authorizationHeader != null)
            {
                string token = await TokenService.GetToken().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue(authorizationHeader.Scheme, token);
            }

            /* send this request to the inner handler */
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}