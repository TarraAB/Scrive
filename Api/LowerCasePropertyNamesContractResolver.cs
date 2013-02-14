using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Scrive.Api
{
    internal class LowerCasePropertyNamesContractResolver : DefaultContractResolver
    {
        public LowerCasePropertyNamesContractResolver() 
            : base(true)
        {
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLowerInvariant();
        }      
    }
}
