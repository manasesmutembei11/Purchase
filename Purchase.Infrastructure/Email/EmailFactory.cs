using Purchase.Domain.IEmail;
using Purchase.Domain.Models.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Purchase.Domain.Templates;
namespace Purchase.Infrastructure.Email
{
    internal class EmailFactory : IEmailFactory
    {

        public string CreateEmail(EmailTemplateType templateType, Dictionary<string, string> dataHolders)
        {
            var template = string.Empty;
            switch (templateType)
            {
                case EmailTemplateType.EmailConfirmation:
                    template = TemplateResource.EmailConfirmation;
                    break;
                case EmailTemplateType.ResetPassword:
                    template = TemplateResource.ResetPassword;
                    break;
            }
            if (string.IsNullOrWhiteSpace(template)) return string.Empty;
            foreach (var d in dataHolders)
            {
                template = template.Replace(d.Key, d.Value);
            }
            return template;
        }
    }
}
