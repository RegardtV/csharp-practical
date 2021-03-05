using System;
using System.Collections.Generic;
using System.Text;

namespace FullStack.ViewModels
{
    public class InvoiceItemModel
    {
        public int Id { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceDescription { get; set; }
        public Decimal ServiceRate { get; set; }
        public double ServiceHours { get; set; }
        public Decimal ServiceCost { get; set; }
        public int InvoiceId { get; set; }
    }
}
