using AuthTemplate.Data;
using AuthTemplate.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTemplate.Web.Email
{
    public interface ISendEmailHelper
    {
        void SendEmailContactForm(ContactFormViewModel contact);
        Task SendAccountEmail(MyApplicationUser user, string link, string text, string btnTitle);
    }
}
