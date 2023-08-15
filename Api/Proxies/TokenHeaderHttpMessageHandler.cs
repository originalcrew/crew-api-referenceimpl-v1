using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Crew.Api.ReferenceImpl.V1.Proxies
{
    /*
     * inspired by Refit.AuthenticatedHttpClientHandler
     */
    public abstract class TokenHeaderHttpMessageHandler : DelegatingHandler
    {
        protected TokenHeaderHttpMessageHandler(ITokenService tokenService, string headerName)
        {
            TokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            HeaderName = headerName ?? throw new ArgumentNullException(nameof(headerName));
        }

        public ITokenService TokenService { get; }
        public string HeaderName { get; set; }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            /* if this request has the specified header, get the token & put it in that header */
            if (request.Headers.Contains(HeaderName))
            {
                string token = await TokenService.GetToken().ConfigureAwait(false);
                request.Headers.Remove(HeaderName);
                request.Headers.Add(HeaderName, token);
            }

            /* send this request to the inner handler */
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}