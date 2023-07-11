using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using RazorEngineCore;

namespace CoreCashApi.Email
{
    public class EmailService
    {
        // TODO: handle email terus lanjutin registrasi
        private readonly EmailSetting _setting;

        private readonly IWebHostEnvironment _env;

        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSetting> setting, ILogger<EmailService> logger, IWebHostEnvironment env)
        {
            _setting = setting.Value;
            _logger = logger;
            _env = env;
        }

        public async Task<bool> SendAsync(EmailModel model, CancellationToken ct = default)
        {
            try
            {
                var mail = new MimeMessage();

                // SENDER
                #region Sender
                mail.From.Add(new MailboxAddress(_setting.FromDisplayName, model.From ?? _setting.From));
                mail.Sender = new MailboxAddress(model.FromDisplayName ?? _setting.FromDisplayName, model.From ?? _setting.From);
                #endregion

                foreach (var mailAddress in model.To)
                {
                    // Mail to
                    mail.To.Add(MailboxAddress.Parse(mailAddress));
                }

                //BCC
                if (model.Bcc != null && model.Bcc!.Count > 0)
                {
                    foreach (string mailAddress in model.Bcc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        mail.Bcc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                    }
                }

                //CC
                if (model.Cc != null && model.Cc!.Count > 0)
                {
                    foreach (string mailAddress in model.Cc.Where(x => !string.IsNullOrWhiteSpace(x)))
                    {
                        mail.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                    }
                }

                // Body
                var body = new BodyBuilder();
                mail.Subject = model.Subject;
                body.HtmlBody = model.Body;
                mail.Body = body.ToMessageBody();

                // Mail sent procces
                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(_setting.Host, _setting.Port, true, ct);

                await smtp.AuthenticateAsync(_setting.Username, _setting.Password, ct);

                await smtp.SendAsync(mail, ct);

                await smtp.DisconnectAsync(true, ct);

                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public string GetEmailTemplate<T>(string emailTemplate, T emailTemplateModel)
        {
            string mailTemplate = LoadTemplate(emailTemplate);

            IRazorEngine razorEngine = new RazorEngine();
            IRazorEngineCompiledTemplate modifiedMailTemplate = razorEngine.Compile(mailTemplate);

            return modifiedMailTemplate.Run(emailTemplateModel);
        }

        public string LoadTemplate(string emailTemplate)
        {
            string baseDir = _env.WebRootPath;
            string templateDir = Path.Combine(baseDir, "email");
            string templatePath = Path.Combine(templateDir, $"{emailTemplate}.cshtml");

            using FileStream fileStream = new(templatePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new(fileStream, Encoding.Default);

            string mailTemplate = streamReader.ReadToEnd();
            streamReader.Close();

            return mailTemplate;
        }
    }
}