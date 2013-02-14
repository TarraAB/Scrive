using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scrive.Api
{
    public class Field
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Obligatory { get; set; }
        public bool Closed { get; set; }
        public List<Placement> Placements { get; private set; }

        public Field()
        {
            Placements = new List<Placement>();
        }
    }
}
