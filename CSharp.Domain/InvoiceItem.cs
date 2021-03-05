using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Domain
{
    /// <summary>
    /// Manages invoice item
    /// </summary>
    public abstract class InvoiceItem
    {
        private double _serviceHours;
        private string _validationMessage;
        public int Id { get; set; }
        public DateTime ServiceDate { get; set; }
        public double ServiceRate { get; protected set; }
        public Invoice Invoice { get; set; }
        public int InvoiceId { get; set; }
        public double ServiceHours
        {
            get { return _serviceHours; }
            // ServiceHours are set in intervals of 0.5
            set
            {
                if (value >= 0)
                {
                    if (value < Math.Floor(value) + 0.5) _serviceHours = Math.Floor(value);
                    else _serviceHours = Math.Floor(value) + 0.5;
                }
                else
                {
                    ValidationMessage = "Service Hours must be a positive value";
                }
            }
        }

        public string ServiceDelivered {get; protected set; }
        public double ServiceCost => ServiceHours * ServiceRate;
        public string ValidationMessage
        {
            get { return _validationMessage; }
            private set
            {
                _validationMessage = value;
                Console.WriteLine(_validationMessage);
            }
        }

        public override string ToString()
        {
            return  $"Date: {ServiceDate.ToShortDateString()}, " +
                    $"Service: {this.ServiceDelivered}, " +
                    $"Hours: {this.ServiceHours}, " +
                    $"Rate: R{this.ServiceRate:0.00} per hour, " +
                    $"Cost: R{this.ServiceCost:0.00}";
        }
        /// <summary>
        /// Displays invoice item on console
        /// </summary>
        public void DisplayItem()
        {
            Console.WriteLine(  $"{ServiceDate.DayOfWeek.ToString(),-10} {ServiceDate.ToShortDateString(),-10}\t|\t" +
                                $"{this.ServiceDelivered,-10}\t|\t" +
                                $"{this.ServiceHours}\t|\t" +
                                $"R{this.ServiceRate:0.00}\t\t|\t" +
                                $"R{this.ServiceCost:0.00}");
        }

    }
    /// <summary>
    /// Manages invoice item of type maintenance
    /// </summary>
    public class MaintenanceInvoiceItem : InvoiceItem
    {
        private const double DEVELOPMENT_RATE = 100.20;
        public MaintenanceInvoiceItem() : base()
        {
            this.ServiceRate = DEVELOPMENT_RATE;
            this.ServiceDelivered = "maintenance";
        }
        public MaintenanceInvoiceItem(DateTime serviceDate, double serviceHours) : this()
        {
            ServiceDate = serviceDate;
            ServiceHours = serviceHours;
        }
    }
    /// <summary>
    /// Manages invoice item of type testing
    /// </summary>
    public class TestingInvoiceItem : InvoiceItem
    {
        private const double TESTING_RATE = 123.45;
        public TestingInvoiceItem() : base()
        {
            this.ServiceRate = TESTING_RATE;
            this.ServiceDelivered = "testing";
        }
        public TestingInvoiceItem(DateTime serviceDate, double serviceHours) : this()
        {
            ServiceDate = serviceDate;
            ServiceHours = serviceHours;
        }

    }
    /// <summary>
    /// Manages invoice item of type optimization
    /// </summary>
    public class OptimizationInvoiceItem : InvoiceItem
    {
        private const double OPTIMIZATION_RATE = 199.99;
        public OptimizationInvoiceItem(): base()
        {
            ServiceRate = OPTIMIZATION_RATE;
            ServiceDelivered = "optimization";
        }
        public OptimizationInvoiceItem(DateTime serviceDate, double serviceHours) : this()
        {
            this.ServiceDate = serviceDate;
            this.ServiceHours = serviceHours;
        }
  
    }
    /// <summary>
    /// Manages invoice item of type development
    /// </summary>
    public class DevelopmentInvoiceItem : InvoiceItem
    {
        private const double DEVELOPMENT_RATE = 320.0;
        public DevelopmentInvoiceItem() : base()
        {
            ServiceRate = DEVELOPMENT_RATE;
            ServiceDelivered = "development";
        }
        public DevelopmentInvoiceItem(DateTime serviceDate, double serviceHours) : this() 
        {
            this.ServiceHours = serviceHours;
            this.ServiceDate = serviceDate;
        }
    }
}

