using System;
using System.IO;
using System.Threading.Tasks;
using Refit;

namespace Crew.Api.ReferenceImpl.V1.Proxies.Documents
{
    public interface IDocumentsApi
    {
        [Get("/version")]
        [Headers("Accept: application/json")]
        Task<string> GetVersion();

        [Post("/generate")]
        Task GenerateDocument([Body] GenerateDocumentRequest request);

        [Get("/{documentId}")]
        Task<Stream> GetDocument(Guid documentId);
    }
}