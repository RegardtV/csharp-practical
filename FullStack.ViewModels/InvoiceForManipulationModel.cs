using System;
using System.Collections.Generic;
using System.Text;

namespace FullStack.ViewModels
{
    public class InvoiceForManipulationModel
    {
        public string RefNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
