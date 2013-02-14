using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace Scrive.Api
{
    public class Client
    {
        readonly string _apiUrl;
        readonly AccessCredentials _credentials;

        public Client(string apiUrl, AccessCredentials credentials)
        {
            _apiUrl = apiUrl;
            _credentials = credentials;
        }

        private RestClient CreateRestClient()
        {
            var client = new RestClient();

            client.BaseUrl = _apiUrl;
            client.AddDefaultHeader("Authorization", _credentials.GetAuthorizationHeader());
            
            // Scrive API doesn't use any specific JSON content-type for the response.
            client.AddHandler("text/plain", new RestSharp.Deserializers.JsonDeserializer());

            return client;
        }

        T Execute<T>(RestRequest request) where T : new()
        {
            var client = CreateRestClient();

            var response = client.Execute<T>(request);

            ThrowOnResponseError(response);

            return response.Data;
        }

        private void ThrowOnResponseError(IRestResponse response)
        {
            if (response.ErrorException != null)
            {
                throw response.ErrorException;
            }

            if ((int)response.StatusCode < 200 || (int)response.StatusCode >= 300)
            {
                throw new ScriveApiException(String.Format("Scrive API returned status {0}.", 
                    response.StatusDescription));
            }
        }

        public Document CreateDocumentFromTemplate(string templateId)
        {
            var request = new RestRequest(String.Format("createfromtemplate/{0}", templateId));

            request.Method = Method.POST;

            return Execute<Document>(request);
        }

        public Document GetDocument(string documentId)
        {
            var request = new RestRequest(String.Format("get/{0}", documentId));

            return Execute<Document>(request);
        }

        public Document UpdateDocument(Document document)
        {
            var request = new RestRequest(String.Format("update/{0}", document.Id));

            request.Method = Method.POST;

            string jsonData = JsonConvert.SerializeObject(document, 
                Formatting.None, 
                new JsonSerializerSettings 
                { 
                    ContractResolver = new LowerCasePropertyNamesContractResolver() 
                });

            var payload = new Parameter
            {
                Name = "json",
                Type = ParameterType.GetOrPost,
                Value = jsonData
            };

            request.Parameters.Add(payload);

            return Execute<Document>(request);
        }

        public Document ReadyDocument(string documentId)
        {
            var request = new RestRequest(String.Format("ready/{0}", documentId));

            request.Method = Method.POST;

            return Execute<Document>(request);
        }

        public byte[] DownloadDocument(string documentId)
        {
            var client = CreateRestClient();
            var request = new RestRequest(String.Format("downloadmainfile/{0}/filename.pdf", documentId));

            return client.DownloadData(request);
        }

        public IList<DocumentListItem> ListDocuments()
        {
            var client = CreateRestClient();
            var request = new RestRequest("list");

            var response = Execute<DocumentList>(request);

            return response.List;
        }

        public void DeleteDocument(string documentId)
        {
            var client = CreateRestClient();
            var request = new RestRequest(String.Format("delete/{0}", documentId));

            request.Method = Method.DELETE;

            var response = client.Execute(request);

            ThrowOnResponseError(response);
        }
    }
}
