using Microsoft.Extensions.Logging;
using MimeKit;
using Purchase.Domain.Contracts.Configs;
using Purchase.Domain.IEmail;
using Purchase.Domain.Models.Configs;
using Purchase.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace Purchase.Infrastructure.Email
{
    internal class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        private EmailConfiguration _emailConfig;
        private readonly IConfigRepository _configRepository;
        public EmailSender(
            ILogger<EmailSender> logger,

            IConfigRepository configRepository
            )
        {
            _logger = logger;

            _configRepository = configRepository;
        }

        public async void SendEmail(Message message)
        {
            _emailConfig = await _configRepository.Load<EmailConfiguration>();
            if (_emailConfig is null)
            {
                _logger.LogDebug("Invalid email server configuration");
                return;
            }
            var emailMessage = CreateEmailMessage(message);
            _logger.LogDebug("Sending email");

            Send(emailMessage);
            _logger.LogDebug("Email  sent");
        }

        public async Task SendEmailAsync(Message message)
        {
            _emailConfig = await _configRepository.Load<EmailConfiguration>();
            if (_emailConfig is null)
            {
                _logger.LogDebug("Invalid email server configuration");
                return;
            }
            _logger.LogDebug("Sending email");
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
            _logger.LogDebug("Email  sent");
        }
        private string temaplate = "<!DOCTYPE html>\r\n<html>\r\n<head>\r\n<title>Email</title>\r\n</head>\r\n\r\n<body>\r\n{0}\r\n</body>\r\n\r\n</html>";
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.Name, _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Cc.AddRange(message.Cc);
            emailMessage.Subject = message.Subject;

            // var bodyBuilder = new BodyBuilder { HtmlBody = string.Format(temaplate, message.Content) };
            var bodyBuilder = new BodyBuilder();
            if (message.IsHtml)
            {
                bodyBuilder.HtmlBody = message.Content;
            }
            else
            {
                bodyBuilder.HtmlBody = string.Format(temaplate, message.Content);
            }

            if (message.Attachments != null && message.Attachments.Any())
            {
                foreach (var attachment in message.Attachments)
                {
                    bodyBuilder.Attachments.Add(attachment.FileName, attachment.Stream.ToArray(), ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.SmtpPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    var password = EncryptDecryptManager.Decrypt(_emailConfig.SmtpPassword);
                    client.Authenticate(_emailConfig.SmtpUserName, password);

                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error when sending email");
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.SmtpPort, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    var password = EncryptDecryptManager.Decrypt(_emailConfig.SmtpPassword);
                    await client.AuthenticateAsync(_emailConfig.SmtpUserName, password);

                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error when sending email");
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}