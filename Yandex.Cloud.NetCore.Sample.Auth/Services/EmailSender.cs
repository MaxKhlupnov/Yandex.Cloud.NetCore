using Microsoft.Extensions.Options;
using Yandex.Cloud.NetCore.Sample.Auth.Models;
using System.Threading.Tasks;
using System;

namespace Yandex.Cloud.NetCore.Sample.Auth.Services
{
    public class EmailSender : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        private readonly IOptions<EmailSettings> _optionsEmailSettings;

        public EmailSender(IOptions<EmailSettings> optionsEmailSettings)
        {
            _optionsEmailSettings = optionsEmailSettings;
        }


        Task Microsoft.AspNetCore.Identity.UI.Services.IEmailSender.SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException("SendEmail not implemented");
        }
    }
}
