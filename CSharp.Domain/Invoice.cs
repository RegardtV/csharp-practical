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
        #region Backing Fields
        private DateTime createDate;
        private List<InvoiceItem> items;
        private string validationMessage;
        #endregion

        #region Constructors
        public Invoice() {
            Items = new List<InvoiceItem>();
            Customer = new Customer();
            // initiate and set CreateDate to 5 days before the end of today's month as default
            var tempDate = DateTime.Today;
            var daysInMonth = DateTime.DaysInMonth(tempDate.Year, tempDate.Month);
            CreateDate = new DateTime(tempDate.Year, tempDate.Month, daysInMonth - 5);
        }
        public Invoice(string refNumber, Customer customer, DateTime createDate): this()
        {
            this.RefNumber = refNumber;
            this.Customer = customer;
            this.CreateDate = createDate;
        }
        #endregion

        #region Properties
        public string RefNumber { get; set; }
        public Customer Customer { get; set; }
        public DateTime CreateDate
        {
            get { return createDate; }
            set 
            {
                // sets date on which invoice was created only if it falls 5 days before the end of a month
                var daysInMonth = DateTime.DaysInMonth(value.Year, value.Month);
                if (value.Day != daysInMonth - 5)
                {
                    ValidationMessage = "Invoice creation date must be 5 days before the end of a month";
                }
                else
                {
                    createDate = value;
                    // iniitate date on which invoice is due based on date on wich it was created
                    // the due date is always at the end of the following month
                    DueDate = createDate.AddMonths(1);
                    var lastDayOfNextMonth = DateTime.DaysInMonth(DueDate.Year, DueDate.Month);
                    DueDate = new DateTime(DueDate.Year, DueDate.Month, lastDayOfNextMonth);
                }
            }
        }
        public DateTime DueDate { get; private set; }
        public double Total => items.Sum(item => item.ServiceCost);
        public List<InvoiceItem> Items
        {
            private get { return items; }
            set
            {
                // sets list of invoice items if item dates falls within valid range for this invoice
                bool isValidListOfItems = true;
                foreach (var item in value)
                {
                    var minDate = CreateDate.AddMonths(-1);
                    var daysInPreviousMonth = DateTime.DaysInMonth(minDate.Year, minDate.Month);
                    minDate = new DateTime(minDate.Year, minDate.Month, daysInPreviousMonth - 5);
                    if (item.ServiceDate < minDate || item.ServiceDate >= CreateDate)
                    {
                        isValidListOfItems = false;
                        Console.WriteLine(item.ServiceDate.ToShortDateString());
                    }
                }
                if (isValidListOfItems) items = value;
                else ValidationMessage = "Item list contains items that fall outside the range of dates for this invoice";
            }
        }
        public string ValidationMessage
        {
            get { return validationMessage; }
            private set 
            { 
                validationMessage = value;
                Console.WriteLine(validationMessage);
            }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Invoice: {RefNumber}, Customer: {Customer}";
        }
        
        /// <summary>
        /// Adds item to invoice items list if within valid range of dates
        /// </summary>
        /// <param name="item">Item to add to items list</param>
        public void AddItem(InvoiceItem item)
        {
            var minDate = CreateDate.AddMonths(-1);
            var daysInPreviousMonth = DateTime.DaysInMonth(minDate.Year, minDate.Month);
            minDate = new DateTime(minDate.Year, minDate.Month, daysInPreviousMonth - 5);
            if (item.ServiceDate < minDate || item.ServiceDate >= CreateDate)
            {
                ValidationMessage = "Item falls outside the range of valid dates for this invoice";
            }
            else
            {
                Items.Add(item);
            }
        }

        /// <summary>
        /// Remove item from items list
        /// </summary>
        /// <param name="item">Item to delete from items list</param>
        public void RemoveItem(InvoiceItem item)
        {
            if (!Items.Remove(item)) ValidationMessage = "item not found in items list";
        }

        /// <summary>
        /// Retrieves list of items as an immutable collection
        /// </summary>
        /// <returns>Items collection</returns>
        public IEnumerable<InvoiceItem> RetrieveItems()
        {
            return Items;
        }

        /// <summary>
        /// Displays invoice on console
        /// </summary>
        public void DisplayInvoice()
        {
            Console.WriteLine($"\nINVOICE: {RefNumber}");
            Console.WriteLine($"ISSUED ON: {CreateDate.DayOfWeek.ToString()} {CreateDate.ToShortDateString()}");
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
            Console.WriteLine($"TOTAL: R{Total:0.00} payable on {DueDate.DayOfWeek.ToString()} {DueDate.Date.ToShortDateString()}\n");
        }
        #endregion
    }
}
  