using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Domain
{
    /// <summary>
    /// Manages invoice details
    /// </summary>
    public class Invoice
    {
        public Invoice() {
            this.items = new List<InvoiceItem>();
        }
        public Invoice(string refNumber, Customer customer, DateTime createDate, DateTime dueDate): this()
        {
            this.RefNumber = refNumber;
            this.Customer = customer;
            this.CreateDate = createDate;
            this.DueDate = dueDate;
        }

        public string RefNumber { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        public double Total => items.Sum(item => item.ServiceCost);
        public List<InvoiceItem> items { get; set; }
        
        public override string ToString()
        {
            return $"Invoice: {RefNumber}, Customer: {Customer}";
        }
        /// <summary>
        /// Displays invoice on console
        /// </summary>
        public void DisplayInvoice()
        {
            Console.WriteLine($"\nINVOICE: {RefNumber}");
            Console.WriteLine($"ISSUED ON: {CreateDate.ToShortDateString()}");
            Console.WriteLine($"CUSTOMER: {Customer.Name}");
            Console.WriteLine($"CONTACT: {Customer.Contact}");
            Console.WriteLine($"ADDRESS: {Customer.Address}");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine(  $"{"DATE", -20}\t|\t" +
                                $"{"SERVICE",-10}\t|\t" +
                                $"HOURS\t|\t" +
                                $"RATE PER HOUR\t|\t" +
                                $"COST");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            foreach (var item in items)
            {
                item.DisplayItem();
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"TOTAL: R{Total:0.00} payable on {DueDate.Date.ToShortDateString()}\n");
        }
    }
}
  