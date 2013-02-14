using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scrive.Api
{
    public class DocumentList
    {
        public List<DocumentListItem> List { get; private set; }

        public DocumentList()
        {
            List = new List<DocumentListItem>();
        }
    }
}
