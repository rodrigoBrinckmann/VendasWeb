using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasWebInfrastructure.Services.EmailService
{
    public class MailConfig
    {
        public string GoogleClientId { get; set; }
        public string GoogleClientSecret { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
    }
}
