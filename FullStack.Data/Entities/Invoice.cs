using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FullStack.Data.Entities
{
    /// <summary>
    /// Manages invoice details
    /// </summary>
    public class Invoice
    {
        public Invoice()
        {
            Items = new List<InvoiceItem>();
        }
        public Invoice(string refNumber) : this()
        {
            this.RefNumber = refNumber;
        }

        public int Id { get; set; }
        public string RefNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public Decimal Total => Items.Sum(item => item.ServiceCost);
        public List<InvoiceItem> Items { get; set; }

        public override string ToString()
        {
            return $"Invoice: {RefNumber}";
        }

        /// <summary>
        /// Adds invoice item to items list
        /// </summary>
        /// <param name="item">Invvoice item to add to items list</param>
        public void AddItem(InvoiceItem item)
        {
            Items.Add(item);
        }

        /// <summary>
        /// Remove invoice item from items list
        /// </summary>
        /// <param name="item">Invoice item to delete from items list</param>
        public void RemoveItem(InvoiceItem item)
        {
            Items.Remove(item);
        }

        /// <summary>
        /// Retrieves list of invoice items
        /// </summary>
        /// <returns>Invoice items list</returns>
        public List<InvoiceItem> RetrieveItems()
        {
            return Items;
        }

        /// <summary>
        /// Retrieves invoice items one at a time
        /// </summary>
        /// <returns>Iterator to invoice items</returns>
        public IEnumerable<InvoiceItem> RetrieveWithIterator()
        {
            foreach (var item in Items)
            {
                yield return item;
            }
        }
        /// <summary>
        /// Displays invoice on console
        /// </summary>
        public void DisplayInvoice()
        {
            Console.WriteLine($"\nINVOICE: {RefNumber}");
            Console.WriteLine($"ISSUED ON: {IssueDate.DayOfWeek.ToString()} {IssueDate.ToShortDateString()}");
            //Console.WriteLine($"CUSTOMER: {Customer.Name}");
            //Console.WriteLine($"CONTACT: {Customer.Contact}");
            //Console.WriteLine($"ADDRESS: {Customer.Address}");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"{"DATE",-20}\t|\t" +
                                $"{"SERVICE",-10}\t|\t" +
                                $"HOURS\t|\t" +
                                $"RATE PER HOUR\t|\t" +
                                $"COST");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            foreach (var item in Items)
            {
                //item.DisplayItem();
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"TOTAL: R{Total:0.00} payable on {DueDate.DayOfWeek.ToString()} {DueDate.Date.ToShortDateString()}\n");
        }
    }
}
