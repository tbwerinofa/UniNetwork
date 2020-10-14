using DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess
{
    [Table(nameof(QuoteStatus), Schema = SchemaName.Finance)]
    public partial class QuoteStatus : AuditBase
    {
        public QuoteStatus()
        {
            this.Quotes = new HashSet<Quote>();
        }

        public string Name { get; set; }
        public string Discriminator { get; set; }

        public bool RequiresPayment { get; set; }
        public string Description { get; set; }
        public int MessageTemplateId { get; set; }

        public virtual ICollection<Quote> Quotes { get; set; }
        public virtual MessageTemplate MessageTemplate { get; set; }
    }
}
