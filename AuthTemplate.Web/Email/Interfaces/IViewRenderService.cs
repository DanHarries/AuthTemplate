using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTemplate.Web.Email
{
    public interface IViewRenderService
    {
        Task<string> RenderToString(string viewName, object model);
    }
}
