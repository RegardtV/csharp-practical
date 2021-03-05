using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp.Domain
{
    /// <summary>
    /// Manages customer details
    /// </summary>
    public class Customer
    {
        public Customer() {}
        public Customer(string name, string address, string contact): this()
        {
            this.Name = name;
            this.Contact = contact;
            this.Address = address;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
    }
}
