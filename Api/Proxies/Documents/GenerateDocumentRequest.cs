using System;
using System.Collections.Generic;

namespace Crew.Api.ReferenceImpl.V1.Proxies.Documents
{
    public class GenerateDocumentRequest
    {
        public string TemplateName { get; set; }
        public string DocumentDataJson { get; set; }
        public string Filename { get; set; }
        public string Reference { get; set; }
        public string ReferenceType { get; set; }
        public int? Priority { get; set; }
        public DateTime? NotBeforeDate { get; set; }
        public DateTime? NotAfterDate { get; set; }
        public SendViaEmailObj SendViaEmail { get; set; }

        public class SendViaEmailObj
        {
            public EmailAddress From { get; set; }
            public EmailAddress ReplyTo { get; set; }
            public IList<string> ToAddresses { get; set; }
            public IList<string> CcAddresses { get; set; }
            public IList<string> BccAddresses { get; set; }
            public string Subject { get; set; }
            public EmailImportance? Importance { get; set; }
            public string Body { get; set; }
            public EmailBodyType? BodyType { get; set; }
            public IList<Attachment> Attachments { get; set; }
            public IList<EmailHeader> Headers { get; set; }
        }

        public class EmailAddress
        {
            public string Address { get; set; }
            public string DisplayName { get; set; }
        }

        public class Attachment
        {
            public string Name { get; set; }
            public byte[] Content { get; set; }
        }

        public class EmailHeader
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}