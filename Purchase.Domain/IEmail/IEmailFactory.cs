using Purchase.Domain.Models.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.IEmail
{
    public interface IEmailFactory
    {
        string CreateEmail(EmailTemplateType templateType, Dictionary<string, string> dataHolders);
    }
}
