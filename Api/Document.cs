using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scrive.Api
{
    public class Document
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public List<Signatory> Signatories { get; private set; }
        public List<string> Tags { get; private set; }

        public string Authentication { get; set; }
        public string Title { get; set; }
        public string Delivery { get; set; }
        public int DaysToSign { get; set; }
        public string InvitationMessage { get; set; }

        public Document()
        {
            Signatories = new List<Signatory>();
            Tags = new List<string>();
        }

        public bool IsValid()
        {
            return Status.ToLower() != "error" && !String.IsNullOrEmpty(Id);
        }
    }
}
