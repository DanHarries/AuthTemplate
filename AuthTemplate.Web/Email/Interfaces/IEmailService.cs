using AuthTemplate.Web.Email.Models;
using MailKit.Net.Smtp;

namespace AuthTemplate.Web.Email
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
    }
}
