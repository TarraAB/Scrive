using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scrive.Example
{
    public class Recipient
    {        
        public string CompanyName { get; set; }
        public string CompanyRegNo { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonLastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
