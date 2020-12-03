using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTemplate.Web.Models
{
    public class EmailViewModel
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
