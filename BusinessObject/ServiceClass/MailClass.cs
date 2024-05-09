using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ServiceClass
{
    public class MailClass
    {
        public string FromMailId { get; set; } = "rectem09@gmail.com";
        public string FromMailIdPassword { get; set; } = "uxictpbgoyizwdhj";
        public List<string> ToMailIds { get; set; } = new List<string>();
        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
        public bool IsBodyHtml { get; set; } = true;
        public List<string> Attachments { get; set; } = new List<string>();
    }
}
