using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.Email.TemplateModel
{
    public class EmailResetPasswordModel : BaseEmailTemplateModel
    {
        public string? EmailAddress { get; set; }

        public string? ResetPasswordToken { get; set; }

        public string? Url { get; set; }
    }
}