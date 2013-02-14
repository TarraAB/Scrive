using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scrive.Api
{
    public class Signatory
    {
        public string Id { get; set; }
        public int SignOrder { get; set; }
        public List<Field> Fields { get; private set; }
        public string Status { get; set; }
        public bool HasUser { get; set; }
        public bool Signs { get; set; }

        public bool Author { get; set; }

        public Signatory()
        {
            Fields = new List<Field>();
        }
    }
}
