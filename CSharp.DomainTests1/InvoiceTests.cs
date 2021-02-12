using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CSharp.Domain.Tests
{
    [TestClass()]
    public class InvoiceTests
    {
        [TestMethod()]
        public void Initialize_CreateDate_To_Five_Days_Before_End_Of_Month_Using_Parameterized_Constructor()
        {
            //Arrange
            var invoice = new Invoice("", new Customer(), new DateTime(2021, 2, 12));
            
            var expected = new DateTime(2021, 2, 23);

            //Act
            var actual = invoice.CreateDate;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Initialize_DueDate_To_End_Of_Following_Month_Using_Parametrized_Constructor()
        {
            //Arrange
            var invoice = new Invoice("", new Customer(), new DateTime(2021, 2, 12));
            
            var expected = new DateTime(2021, 3, 31);

            //Act
            var actual = invoice.DueDate;

            //Assert
            Assert.AreEqual(expected, actual);
        }
        
        [TestMethod()]
        public void Initialize_CreateDate_To_Five_Days_Before_End_Of_Month_Using_CreateDate_Setter()
        {
            //Arrange
            var invoice = new Invoice();
            invoice.CreateDate = new DateTime(2021, 2, 12);

            var expected = new DateTime(2021, 2, 23);

            //Act
            var actual = invoice.CreateDate;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Initialize_DueDate_To_End_Of_Following_Month_Using_CreateDate_Setter()
        {
            //Arrange
            var invoice = new Invoice();
            invoice.CreateDate = new DateTime(2021, 2, 12);
            
            var expected = new DateTime(2021, 3, 31);

            //Act
            var actual = invoice.DueDate;

            //Assert
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void Do_Not_Assign_Items_To_List_That_Contains_Date_Before_CreateDate_Of_Previous_Month()
        {
            //Arrange
            var invoice = new Invoice("", new Customer(), new DateTime(2021, 2, 12));

            var list = new List<InvoiceItem>();
            list.Add(new MaintenanceInvoiceItem(new DateTime(2021, 1, 25), 1));
            invoice.Items = list;
            
            var expected = "Item list contains items that fall outside the range of dates for this invoice";

            //Act
            var actual = invoice.ValidationMessage;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Do_Not_Assign_Items_To_List_That_Contains_Date_On_Or_After_CreateDate_Of_Current_Month()
        {
            //Arrange
            var invoice = new Invoice("", new Customer(), new DateTime(2021, 2, 12));

            var list = new List<InvoiceItem>();
            list.Add(new MaintenanceInvoiceItem(new DateTime(2021, 2, 23), 1));
            invoice.Items = list;
            
            var expected = "Item list contains items that fall outside the range of dates for this invoice";

            //Act
            var actual = invoice.ValidationMessage;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Assign_Items_To_List_That_Contains_Date_On_CreateDate_Of_Previous_Month()
        {
            //Arrange
            var invoice = new Invoice("", new Customer(), new DateTime(2021, 2, 12));

            var list = new List<InvoiceItem>();
            list.Add(new MaintenanceInvoiceItem(new DateTime(2021, 1, 26), 1));
            invoice.Items = list;

            var expected = list;

            //Act
            var actual = invoice.RetrieveItems();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Assign_Items_To_List_That_Contains_Date_A_Day_Before_CreateDate_Of_Current_Month()
        {
            //Arrange
            var invoice = new Invoice("", new Customer(), new DateTime(2021, 2, 12));

            var list = new List<InvoiceItem>();
            list.Add(new MaintenanceInvoiceItem(new DateTime(2021, 2, 22), 1));
            invoice.Items = list;

            var expected = list;

            //Act
            var actual = invoice.RetrieveItems();

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Do_Not_Add_Date_To_Items_If_Date_Is_Before_CreateDate_Of_Previous_Month()
        {
            //Arrange
            var invoice = new Invoice("", new Customer(), new DateTime(2021, 2, 12));
            invoice.AddItem(new MaintenanceInvoiceItem(new DateTime(2021, 1, 25), 1));

            var expected = "Item falls outside the range of valid dates for this invoice";

            //Act
            var actual = invoice.ValidationMessage;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Do_Not_Add_Date_To_Items_If_Date_Is_On_Or_After_CreateDate_Of_Current_Month()
        {
            //Arrange
            var invoice = new Invoice("", new Customer(), new DateTime(2021, 2, 12));
            invoice.AddItem(new MaintenanceInvoiceItem(new DateTime(2021, 2, 23), 1));

            var expected = "Item falls outside the range of valid dates for this invoice";

            //Act
            var actual = invoice.ValidationMessage;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Add_Date_To_Items_If_Date_Is_On_CreateDate_Of_Previous_Month()
        {
            //Arrange
            var invoice = new Invoice("", new Customer(), new DateTime(2021, 2, 12));
            var item = new MaintenanceInvoiceItem(new DateTime(2021, 2, 2), 1);
            invoice.AddItem(item);

            var list = new List<InvoiceItem>();
            list.Add(item);

            var expected = list;

            //Act;
            var actual = invoice.RetrieveItems().ToList();
            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Add_Date_To_Items_If_Date_Is_A_Day_Before_CreateDate_Of_Current_Month()
        {
            //Arrange
            var invoice = new Invoice("", new Customer(), new DateTime(2021, 2, 12));
            var item = new MaintenanceInvoiceItem(new DateTime(2021, 2, 22), 1);
            invoice.AddItem(item);

            var list = new List<InvoiceItem>();
            list.Add(item);

            var expected = list;

            //Act;
            var actual = invoice.RetrieveItems().ToList();
            //Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}