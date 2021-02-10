using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Domain;

namespace CSharp.App
{
    class Program
    {
        // Static field used to generate number part of invoice reference number
        static int invoiceNumber = 0;
        
        /// <summary>
        /// Generates 12 invoices
        /// </summary>
        /// <returns>Iterator to an Inovice object</returns>
        static IEnumerable<Invoice> invoiceGenerator()
        {
            var startingDate = DateTime.Today;
            int lastDayOfCurrentMonth;
            int lastDayOfNextMonth;

            DateTime currentDate;
            DateTime createDate;
            DateTime nextDate;
            DateTime dueDate;
           
            Invoice nextInvoice;

            // loop to generate 12 invoices
            for (int i = 0; i < 12; i++)
            {
                // initiate date on which invoice was issued
                // always 5 days before end of month
                currentDate = startingDate.AddMonths(i);
                lastDayOfCurrentMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
                createDate = new DateTime(currentDate.Year, currentDate.Month, lastDayOfCurrentMonth - 5);
                
                // iniitate date on which invoice is due
                // always at the end of the following month
                nextDate = currentDate.AddMonths(1);
                lastDayOfNextMonth = DateTime.DaysInMonth(nextDate.Year, nextDate.Month);
                dueDate = new DateTime(nextDate.Year, nextDate.Month, lastDayOfNextMonth);
                
                // initiate invoice 
                nextInvoice = new Invoice($"{invoiceNumber++:000}INV", getCustomer(), createDate, dueDate);
                
                // retrieve iterator to an item list and assign item list to invoice
                var invoiceItemsIterator = invoiceItemsGenerator(createDate);
                foreach (var items in invoiceItemsIterator)
                {
                    nextInvoice.items = items;
                }
                // yield an invoice iterator
                yield return nextInvoice;
            }
        }

        /// <summary>
        /// Returns a random customer from a list of customers
        /// </summary>
        /// <returns>Customer object</returns>
        static Customer getCustomer()
        {
            Random rnd = new Random();
            var customers = new List<Customer>();
            
            customers.Add(new Customer("Solid Solutions", "24 Old Oak avenue, Bloemfontein, 7301", "0841582488"));
            customers.Add(new Customer("Vega Inc", "08 Cauldron avenue, Johnsontown, 1501", "0838921485"));
            customers.Add(new Customer("Jonathan Davies", "17 Church street, Kingston, 2881", "0848529631"));
            customers.Add(new Customer("Fairscape Landscaping", "52 Brown road, Bloemfontein, 7301", "0795287535"));
            customers.Add(new Customer("Anagram Analytics", "18 Robert Frost avenue, Kilganon, 1126", "0837832654"));
            customers.Add(new Customer("ABC logistics", "05 Rainforest road, Blairville, 8485", "0796295841"));
            customers.Add(new Customer("Crucible Control", "10 Steel street, Inkwezi, 5811", "0841239874"));
            customers.Add(new Customer("Craig Longbottom", "25 Lonsdale road, King William's Town, 7333", "0825698123"));
            customers.Add(new Customer("Savesmart Bank", "17 Alexandra street, George, 6812", "0826834256"));
            customers.Add(new Customer("Core AI", "42 Rembrandt avenue, Cape Town, 7550", "0798696823"));
            customers.Add(new Customer("Breezy", "11 Downing avenue, East London, 3526", "0837853474"));
            customers.Add(new Customer("Acclaimed Accounting", "22 Bunting road, Simonsville, 3563", "0792837192"));
            customers.Add(new Customer("Brent Cloete", "01 Tavern street, Peddie, 5363", "0846938562"));
            customers.Add(new Customer("La Russo Automobiles", "37 Picasso lane, Paarl, 7553", "0823214568"));
            customers.Add(new Customer("MediCare", "19 Prim street, Lockdon, 6562", "0829516232"));
            customers.Add(new Customer("Jessica Langley", "31 Quick avenue, Cape Town, 7550", "0792266631"));
            customers.Add(new Customer("Sayyes!", "14 Vatnir avenue, East London, 3526", "083556233"));
            customers.Add(new Customer("Dartmouth College", "28 Ovencloak road, Geartown, 2226", "0796698852"));
            
            return customers[rnd.Next(0,customers.Count())];
        }

        /// <summary>
        ///  Generates lists of invoice items and retrieves the lists with an iterator one at a time
        /// </summary>
        /// <param name="invoiceDate">The date on which the invoice was issued</param>
        /// <returns>Iterator to an item list</returns>
        static IEnumerable<List<InvoiceItem>> invoiceItemsGenerator(DateTime invoiceDate)
        {
            InvoiceItem tempItem;
            DateTime itemDate;
            var itemList = new List<InvoiceItem>();
            Random rnd = new Random();

            var aDateInPreviousMonth = invoiceDate.AddMonths(-1);
            var daysInPreviousMonth = DateTime.DaysInMonth(aDateInPreviousMonth.Year, aDateInPreviousMonth.Month);
            
            // loop to generate 12 lists of invoice items for each invoice
            for (int i = 0; i < 12; i++)
            {
                itemList.Clear();
                // loop to generate a list of invoice items
                // each list contain between 1 and 7 items
                for (int j = 0; j < rnd.Next(1, 8); j++)
                {
                    // initiate random item date for each invoice item within month on which invoice was issued
                    // also ensure item date do not fall on weekends
                    do
                    {
                        var itemDay = rnd.Next(1, daysInPreviousMonth);
                        // if item date falls after 5 days before end of the previous month it 
                        // has to be included in the invoice of the current month
                        if (itemDay >= daysInPreviousMonth - 5)
                        {
                            itemDate = new DateTime(aDateInPreviousMonth.Year, aDateInPreviousMonth.Month, itemDay);
                        }
                        else
                        {
                            itemDate = new DateTime(invoiceDate.Year, invoiceDate.Month, itemDay);
                        }
                    } while (itemDate.DayOfWeek == DayOfWeek.Saturday || itemDate.DayOfWeek == DayOfWeek.Sunday);

                    // initate random invoice item type
                    // each invoice item contains a service hours property that ranges from 1 to 8 hours
                    switch (rnd.Next(1, 5))
                    {
                        case 1:
                            tempItem = new MaintenanceInvoiceItem(itemDate, rnd.Next(1, 9));
                            itemList.Add(tempItem);
                            break;
                        case 2:
                            tempItem = new TestingInvoiceItem(itemDate, rnd.Next(1, 9));
                            itemList.Add(tempItem);
                            break;
                        case 3:
                            tempItem = new OptimizationInvoiceItem(itemDate, rnd.Next(1, 9));
                            itemList.Add(tempItem);
                            break;
                        case 4:
                            tempItem = new DevelopmentInvoiceItem(itemDate, rnd.Next(1, 9));
                            itemList.Add(tempItem);
                            break;

                    }
                }
            }
            // order item list by item date before yielding an iterator to an item list
            yield return itemList.OrderBy(item => item.ServiceDate).ToList();
        }

        static void Main(string[] args)
        {
            // retrieve iterator to an invoice object and display invoice
            var invoiceIterator = invoiceGenerator();
            foreach (var inv in invoiceIterator)
            {
                inv.DisplayInvoice();
            }
        }
    }
}
