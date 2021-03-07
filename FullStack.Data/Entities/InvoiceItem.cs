using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FullStack.Data.Entities
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public DateTime ServiceDate { get; set; }
        public string ServiceDescription { get; set; }
        public Decimal ServiceRate { get; set; }
        public double ServiceHours { get; set; }
        public Decimal ServiceCost => (decimal)ServiceHours * ServiceRate;
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
