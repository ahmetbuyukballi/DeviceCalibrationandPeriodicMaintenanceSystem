using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto.MailServiceDto
{
    public class MailDto
    {
        public string toEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
