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
        private DateTime _createDate;
        private List<InvoiceItem> _items;
        private string _validationMessage;
        #endregion

        #region Constructors
        public Invoice() {
            Items = new List<InvoiceItem>();
            Customer = new Customer();
            CreateDate = DateTime.Today;
        }
        public Invoice(string refNumber, Customer customer, DateTime createDate): this()
        {
            this.RefNumber = refNumber;
            this.Customer = customer;
            this.CreateDate = createDate;
        }
        #endregion

        #region Properties
        public int Id { get; set; }
        public string RefNumber { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreateDate
        {
            get { return _createDate; }
            set 
            {
                // set CreateDate to 5 days before the end of the month of the date that was supplied
                var daysInMonth = DateTime.DaysInMonth(value.Year, value.Month);
                _createDate = new DateTime(value.Year, value.Month, daysInMonth - 5);
                
                // initiate date on which invoice is due based on date on wich it was created
                // the due date is always at the end of the following month
                DueDate = _createDate.AddMonths(1);
                var lastDayOfNextMonth = DateTime.DaysInMonth(DueDate.Year, DueDate.Month);
                DueDate = new DateTime(DueDate.Year, DueDate.Month, lastDayOfNextMonth);
                
            }
        }
        public DateTime DueDate { get; private set; }
        public double Total => _items.Sum(item => item.ServiceCost);
        public List<InvoiceItem> Items
        {
            private get { return _items; }
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
                if (isValidListOfItems) _items = value;
                else ValidationMessage = "Item list contains items that fall outside the range of dates for this invoice";
            }
        }
        public string ValidationMessage
        {
            get { return _validationMessage; }
            private set 
            { 
                _validationMessage = value;
                Console.WriteLine(_validationMessage);
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
        /// Retrieves items one at a time
        /// </summary>
        /// <returns>Iterator to InvoiceItem</returns>
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
            foreach (var item in Items)
            {
                item.DisplayItem();
            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine($"TOTAL: R{Total:0.00} payable on {DueDate.DayOfWeek.ToString()} {DueDate.Date.ToShortDateString()}\n");
        }
        #endregion
    }
}
   