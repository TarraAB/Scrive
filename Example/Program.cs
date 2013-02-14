using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Scrive.Api;

namespace Scrive.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var recipient = new Recipient
            {
                CompanyName = "Acme Inc.",
                CompanyRegNo = "123456-7890",
                EmailAddress = "sven.banan@example.com",
                PersonFirstName = "Sven",
                PersonLastName = "Banan",
                ExpiryDate = new DateTime(2013, 12, 24)
            };

            var document = CreateAndReadyDocument("1234567890", recipient);

            Console.WriteLine("New document having ID {0} created from template and sent to {1}.",
                document.Id, recipient.EmailAddress);
        }

        static Document CreateAndReadyDocument(string templateId, Recipient recipient)
        {
            var credentials = new AccessCredentials();

            // TODO: fill in your personal access credentials (or OAuth credentials?)

            var client = new Client("https://scrive.com/api/v1/", credentials);

            var document = client.CreateDocumentFromTemplate(templateId);

            if (document.Status.ToLower() != "preparation")
            {
                throw new ScriveApiException("Failed to create document from template.", document);
            }

            // Fill in some standard fields
            document.Signatories[1].Fields.Single(n => n.Name == "fstname").Value = recipient.PersonFirstName;
            document.Signatories[1].Fields.Single(n => n.Name == "sndname").Value = recipient.PersonLastName;
            document.Signatories[1].Fields.Single(n => n.Name == "email").Value = recipient.EmailAddress;
            document.Signatories[1].Fields.Single(n => n.Name == "sigco").Value = recipient.CompanyName;
            document.Signatories[1].Fields.Single(n => n.Name == "sigcompnr").Value = recipient.CompanyRegNo;

            // Fill in a custom field (defined in template)
            document.Signatories[1].Fields.Single(n => n.Name == "c_expirydate").Value = recipient.ExpiryDate.ToString("yyyy-MM-dd");

            var updatedDocument = client.UpdateDocument(document);

            if (updatedDocument.Status.ToLower() != "preparation")
            {
                throw new ScriveApiException("Failed to update document.", document);
            }

            var readyDocument = client.ReadyDocument(updatedDocument.Id);

            if (readyDocument.Status.ToLower() != "pending")
            {
                throw new ScriveApiException("Failed to ready document for signing.", document);
            }

            return readyDocument;
        }
    }
}
