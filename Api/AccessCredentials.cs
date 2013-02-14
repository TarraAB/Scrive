using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Scrive.Api
{
    public class AccessCredentials
    {
        public string ClientCredentialsIdentifier { get; set; }
        public string ClientCredentialsSecret { get; set; }
        public string TokenCredentialsIdentifier { get; set; }
        public string TokenCredentialsSecret { get; set; }

        public string GetAuthorizationHeader()
        {
            var result = new StringBuilder();

            result.Append("OAuth realm=\"Scrive\", ");
            result.Append("oauth_signature_method=\"PLAINTEXT\", ");
            result.AppendFormat("oauth_consumer_key=\"{0}\", ", ClientCredentialsIdentifier);
            result.AppendFormat("oauth_signature=\"{0}&{1}\", ", ClientCredentialsSecret, TokenCredentialsSecret);
            result.AppendFormat("oauth_token=\"{0}\", ", TokenCredentialsIdentifier);
            result.Append("privileges=\"DOC_CREATE+DOC_SEND+DOC_CHECK\"");

            return result.ToString();
        }
    }
}
