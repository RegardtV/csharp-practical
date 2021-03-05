using System;
using System.Collections.Generic;
using System.Text;

namespace FullStack.Data.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string RefNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Total { get; set; }
        public ICollection<InvoiceItem> Items { get; set; }
            = new List<InvoiceItem>();
    }
}
