using System;
using System.Net.Http;

namespace Crew.Api.ReferenceImpl.V1.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// <para>*** This is a better version of <see cref="HttpResponseMessage.EnsureSuccessStatusCode" />. ***</para>
        /// <para>Throws a <see cref="HttpRequestException" /> if the <see cref="HttpResponseMessage.IsSuccessStatusCode" /> property for the HTTP response is <see langword="false" />.</para>
        /// </summary>
        public static HttpResponseMessage EnsureSuccessStatusCode2(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                string errMsg =
                    $"Response status code from {response.RequestMessage?.Method} {response.RequestMessage?.RequestUri} does not indicate success: {(int)response.StatusCode} ({response.ReasonPhrase}).{Environment.NewLine}{response.Content?.ReadAsStringAsync().ConfigureAwait(false).GetAwaiter().GetResult()}";

                throw new HttpRequestException(errMsg)
                {
                    Data =
                    {
                        { "StatusCode", response.StatusCode }
                    }
                };
            }

            return response;
        }
    }
}