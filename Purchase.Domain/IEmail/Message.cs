using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.IEmail
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; } = new List<MailboxAddress>();
        public List<MailboxAddress> Cc { get; set; } = new List<MailboxAddress>();
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsHtml { get; private set; }
        public void AddAddressCC(IEnumerable<string> cc)
        {
            if (cc != null)
            {
                Cc.AddRange(cc.Select(x => new MailboxAddress(x, x)));
            }
        }


        public List<EmailAttachment> Attachments { get; } = new List<EmailAttachment>();
        public Message(IEnumerable<string> to, string subject, string content)
        {


            To.AddRange(to.Select(x => new MailboxAddress(x, x)));
            Subject = subject;
            Content = content;

        }
        public Message(IEnumerable<string> to, string subject, string content, bool isHtml)
        {
            IsHtml = isHtml;

            To.AddRange(to.Select(x => new MailboxAddress(x, x)));
            Subject = subject;
            Content = content;

        }
    }
    public class EmailAttachment
    {
        public EmailAttachment(MemoryStream stream, string fileName, string contentType)
        {
            Stream = stream;
            FileName = fileName;
            ContentType = contentType;
        }
        public MemoryStream Stream { get; }
        public string FileName { get; }
        public string ContentType { get; }


    }
}
