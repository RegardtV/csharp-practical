using System;
using System.Collections.Generic;
using System.Text;
using FullStack.Data.Entities;

namespace FullStack.ViewModels
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public string RefNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Total { get; set; }
        public ICollection<InvoiceItemModel> Items { get; set; }
            = new List<InvoiceItemModel>();
    }
}
