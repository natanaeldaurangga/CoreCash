using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCashApi.Email.TemplateModel
{
    public abstract class BaseEmailTemplateModel
    {
        public string? LogoUrl { get; set; }

        public string? BackgroundImageUrl { get; set; }
    }
}