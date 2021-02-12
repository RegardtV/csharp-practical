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
        static IEnumerable<Invoice> InvoiceGenerator()
        {
            var startingDate = DateTime.Today;
            DateTime currentDate;
            Invoice nextInvoice;

            // loop to generate 12 invoices
            for (int i = 0; i < 12; i++)
            {
                // initiate date on which invoice was issued
                currentDate = startingDate.AddMonths(i);                
                // initiate invoice 
                nextInvoice = new Invoice($"INV{invoiceNumber++:000}", GetCustomer(), currentDate);
                
                // retrieve iterator to an item list and assign item list to invoice
                var invoiceItemsIterator = InvoiceItemsGenerator(currentDate);
                foreach (var items in invoiceItemsIterator)
                {
                    nextInvoice.Items = items;
                }
                // yield an invoice iterator
                yield return nextInvoice;
            }
        }

        /// <summary>
        /// Returns a random customer from a list of customers
        /// </summary>
        /// <returns>Customer object</returns>
        static Customer GetCustomer()
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
        static IEnumerable<List<InvoiceItem>> InvoiceItemsGenerator(DateTime invoiceDate)
        {
            InvoiceItem tempItem;
            DateTime itemDate;
            var itemList = new List<InvoiceItem>();
            Random rnd = new Random();

            var invoiceDateOfPreviousMonth = invoiceDate.AddMonths(-1);
            var daysInPreviousMonth = DateTime.DaysInMonth(invoiceDateOfPreviousMonth.Year, invoiceDateOfPreviousMonth.Month);
            invoiceDateOfPreviousMonth = new DateTime(invoiceDateOfPreviousMonth.Year, invoiceDateOfPreviousMonth.Month, daysInPreviousMonth - 5);
            TimeSpan timeSpanBetweenInvoiceDates = invoiceDate - invoiceDateOfPreviousMonth;
 
            // loop to generate 12 lists of invoice items for each invoice
            for (int i = 0; i < 12; i++)
            {
                itemList.Clear();
                // loop to generate a list of invoice items
                // each list contain between 1 and 10 items
                for (int j = 0; j < rnd.Next(1, 11); j++)
                {
                    // initiate random item date for each invoice item that falls wihitn valid date range
                    // also ensure item date do not fall on weekends
                    do
                    {
                        TimeSpan tempTimeSpan = new TimeSpan(0, rnd.Next(0, (int)timeSpanBetweenInvoiceDates.TotalMinutes), 0);
                        itemDate = invoiceDateOfPreviousMonth + tempTimeSpan;
                    } while (itemDate.DayOfWeek == DayOfWeek.Saturday || itemDate.DayOfWeek == DayOfWeek.Sunday);

                    // initate random invoice item type
                    // each invoice item contains a service hours property that ranges from 0.5 to 8 hours
                    switch (rnd.Next(1, 5))
                    {
                        case 1:
                            tempItem = new MaintenanceInvoiceItem(itemDate, rnd.NextDouble() * (8.4 - 0.5) + 0.5);
                            itemList.Add(tempItem);
                            break;
                        case 2:
                            tempItem = new TestingInvoiceItem(itemDate, rnd.NextDouble() * (8.4 - 0.5) + 0.5);
                            itemList.Add(tempItem);
                            break;
                        case 3:
                            tempItem = new OptimizationInvoiceItem(itemDate, rnd.NextDouble() * (8.4 - 0.5) + 0.5);
                            itemList.Add(tempItem);
                            break;
                        case 4:
                            tempItem = new DevelopmentInvoiceItem(itemDate, rnd.NextDouble() * (8.4 - 0.5) + 0.5);
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
            var invoiceIterator = InvoiceGenerator();
            foreach (var inv in invoiceIterator)
            {
                inv.DisplayInvoice();
            }
        }
    }
}
