using DataAccess.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(Document), Schema = SchemaName.Meta)]
    public class Document:AuditBase
    {

        public Document()
        {
            this.Trophies = new HashSet<Trophy>();
            this.ProductImages = new HashSet<ProductImage>();
            this.SystemDocuments = new HashSet<SystemDocument>();
            this.People = new HashSet<Person>();
        }


        public int DocumentTypeId { get; set; }

        public byte[] DocumentData { get; set; }

        public string Name { get; set; }

        public string DocumentNameGuid { get; set; }

        public string Comments { get; set; }

        public virtual DocumentType DocumentType { get; set; }

        public virtual ICollection<Trophy> Trophies { get; set; }

        public virtual ICollection<ProductImage> ProductImages { get; set; }

        public virtual ICollection<SystemDocument> SystemDocuments { get; set; }
        public virtual ICollection<Person> People { get; set; }

    }
}