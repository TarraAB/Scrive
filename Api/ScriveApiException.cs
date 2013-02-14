using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Scrive.Api;

namespace Scrive.Api
{
    [Serializable]
    public class ScriveApiException : Exception 
    {
        public Document Document
        {
            get;
            private set;
        }

        public ScriveApiException()
        {
        }

        public ScriveApiException(string message)
            : base(message)
        {
        }

        public ScriveApiException(string message, Document document)
            : base(message)
        {
            Document = document;
        }

        public ScriveApiException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ScriveApiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
