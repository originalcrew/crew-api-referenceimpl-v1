using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Crew.Api.ReferenceImpl.V1.Extensions;
using Crew.Api.ReferenceImpl.V1.Messages;
using MediatR;

namespace Crew.Api.ReferenceImpl.V1.Handlers
{
    public class TestUrlHandler : IRequestHandler<TestUrlCommand, DiagnosticsResult>
    {
        public TestUrlHandler(IHttpClientFactory clientFactory)
        {
            ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public IHttpClientFactory ClientFactory { get; }

        public async Task<DiagnosticsResult> Handle(TestUrlCommand command, CancellationToken cancellationToken)
        {
            DiagnosticsResult result;

            try
            {
                HttpClient client = ClientFactory.CreateClient();

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(command.AcceptHeader));

                HttpResponseMessage response = await client.GetAsync(command.Url, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode2();

                result = new DiagnosticsResult
                {
                    Key = command.Key,
                    Ok = true
                };
            }
            catch (Exception ex)
            {
                result = new DiagnosticsResult
                {
                    Key = command.Key,
                    Ok = false,
                    Message = ex.Message
                };
            }

            return result;
        }
    }
}