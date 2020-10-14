using System.Collections.Generic;

namespace DataAccess
{
    public class DocumentType:AuditBase
    {
        public DocumentType()
        {
            this.Documents = new HashSet<Document>();
        }


        public string Name { get; set; }

        public string Discriminator { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

    }
}