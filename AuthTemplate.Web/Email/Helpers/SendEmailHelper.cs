using AuthTemplate.Data;
using AuthTemplate.Web.Email.Models;
using AuthTemplate.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTemplate.Web.Email.Helpers
{
    public class SendEmailHelper : ISendEmailHelper
    {
        private readonly IEmailService _email;
        private readonly IConfiguration _config;
        private readonly ILogger<SendEmailHelper> _logger;
        private readonly IViewRenderService _emailView;

        public SendEmailHelper(
            IEmailService email, 
            IConfiguration config, 
            ILogger<SendEmailHelper> logger, 
            IViewRenderService emailView)
        {
            _email = email;
            _config = config;
            _logger = logger;
            _emailView = emailView;
        }

        /// <summary>
        /// Send confirmation account email 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="link"></param>
        /// <param name="text"></param>
        /// <param name="btnTitle"></param>
        /// <returns></returns>
        public async Task SendAccountEmail(MyApplicationUser user, string link, string text, string btnTitle)
        {
            try
            {
                var emailModel = new EmailViewModel
                {
                    Text = text,
                    Url = link,
                    Title = btnTitle
                };

                var emailHtml = await _emailView.RenderToString("~/Views/Email/EmailLayout.cshtml", emailModel);

                var msg = new EmailMessage
                {
                    Content = emailHtml,
                    Subject = emailModel.Title

                };
                msg.ToAddresses.Add(new EmailAddress
                {
                    Name = user.UserName,
                    Address = user.Email
                });

                msg.FromAddresses.Add(new EmailAddress
                {
                    Name = "My App Name/ Company",
                    Address = "noreply@test.com"
                });

                _email.Send(msg);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                throw ex;
            }
            
        }

        public void SendEmailContactForm(ContactFormViewModel contact)
        {
            throw new NotImplementedException();
        }
    }
}
