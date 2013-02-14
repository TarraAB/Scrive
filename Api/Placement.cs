using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scrive.Api
{
    public class Placement
    {
        public double XRel { get; set; }
        public double YRel { get; set; }
        public double WRel { get; set; }
        public double HRel { get; set; }
        public double FSRel { get; set; }
        public int Page { get; set; }
        public string Tip { get; set; }
    }
}
