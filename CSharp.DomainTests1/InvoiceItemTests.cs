using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Domain.Tests
{
    [TestClass()]
    public class InvoiceItemTests
    {
        [TestMethod()]
        public void Do_Not_Set_ServiceHours_If_Value_Is_Negative_Using_Constructor()
        {
            //Arrange
            var item = new MaintenanceInvoiceItem(new DateTime(2021, 2, 22), -2.6);

            var expected = "Service Hours must be a positive value";

            //Act;
            var actual = item.ValidationMessage;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Do_Not_Set_ServiceHours_If_Value_Is_Negative_Using_Setter()
        {
            //Arrange
            var item = new MaintenanceInvoiceItem();
            item.ServiceHours = -0.3;

            var expected = "Service Hours must be a positive value";

            //Act;
            var actual = item.ValidationMessage;

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Set_ServiceHours_To_The_Nearest_Point_Five_Rounded_Down_Using_Constructor()
        {
            //Arrange
            var item = new MaintenanceInvoiceItem(new DateTime(2021, 2, 22), 2.6);

            var expected = 2.5;

            //Act;
            var actual = item.ServiceHours;
            
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Set_ServiceHours_To_The_Nearest_Point_Five_Rounded_Down_Using_Setter()
        {
            //Arrange
            var item = new MaintenanceInvoiceItem(new DateTime(2021, 2, 22), 0.3);

            var expected = 0.0;

            //Act;
            var actual = item.ServiceHours;

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}