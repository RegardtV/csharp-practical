using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FullStack.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullStack.Data
{
    public interface IFullStackRepository
    {
        List<User> GetUsers();
        User GetUser(int userId);
        User CreateUser(User user);
        User UpdateUser(User user);
        void DeleteUser(int userId);

        // Customer methods
        List<Customer> GetCustomers();
        Customer GetCustomer(int customerId);
        Customer CreateCustomer(Customer customer);
        Customer UpdateCustomer(int customerId, Customer customer);
        bool DeleteCustomer(int customerId);

        // Invoice methods
        List<Invoice> GetInvoices();
        Invoice GetInvoice(int invoiceId);
        Invoice CreateInvoice(Invoice invoice);
        Invoice UpdateInvoice(int invoiceId, Invoice invoice);
        bool DeleteInvoice(int invoiceId);

        // InvoiceItem methods
        List<InvoiceItem> GetInvoiceItems(int invoiceId);
        InvoiceItem GetInvoiceItem(int invoiceId, int invoiceItemId);
        InvoiceItem CreateInvoiceItem(int invoiceId, InvoiceItem invoiceItem);
        InvoiceItem UpdateInvoiceItem(int invoiceItemId, InvoiceItem invoiceItem);
        bool DeleteInvoiceItem(int invoiceItemId);
    }
    public class FullStackRepository: IFullStackRepository
    {
        private readonly FullStackDbContext _ctx;
        public FullStackRepository(FullStackDbContext ctx)
        {
            _ctx = ctx;
        }
        #region User methods
        public List<User> GetUsers()
        {
            return _ctx.Users.ToList();
        }
        public User GetUser(int userId)
        {
            return _ctx.Users.Find(userId);
        }
        public User CreateUser(User user)
        {
            _ctx.Users.Add(user);
            _ctx.SaveChanges();

            return user;
        }
        public User UpdateUser(User user)
        {
            var existing = _ctx.Users.SingleOrDefault(u => u.Id == user.Id);
            if (existing == null) return null;

            _ctx.Entry(existing).State = EntityState.Detached;
            _ctx.Users.Attach(user);
            _ctx.Entry(user).State = EntityState.Modified;
            _ctx.SaveChanges();

            return user;
        }
        public void DeleteUser(int userId)
        {
            var entity = _ctx.Users.Find(userId);
            _ctx.Users.Remove(entity);
            _ctx.SaveChanges();
        }
        #endregion

        #region Customer methods
        public List<Customer> GetCustomers()
        {
            return _ctx.Customers.ToList();
        }
        public Customer GetCustomer(int customerId)
        {
            return _ctx.Customers.Find(customerId);
        }
        public Customer CreateCustomer(Customer customer)
        {
            _ctx.Customers.Add(customer);
            _ctx.SaveChanges();
            
            return customer;
        }
        public Customer UpdateCustomer(int customerId, Customer customer)
        {
            var existing = _ctx.Customers.SingleOrDefault(c => c.Id == customerId);
            if (existing == null) return null;

            customer.Id = existing.Id;
            _ctx.Entry(existing).State = EntityState.Detached;
            _ctx.Customers.Update(customer);
            _ctx.SaveChanges();
            
            return customer;
        }
        public bool DeleteCustomer(int customerId)
        {
            var entity = _ctx.Customers.Find(customerId);
            if (entity == null)
            {
                return false;
            }
            
            _ctx.Customers.Remove(entity);
            _ctx.SaveChanges();
            
            return true;
        }
        #endregion

        #region Invoice methods
        public List<Invoice> GetInvoices()
        {
            return _ctx.Invoices.Include(inv => inv.Items).ToList();
        }
        public Invoice GetInvoice(int invoiceId)
        {
            return _ctx.Invoices.Include(inv => inv.Items).SingleOrDefault(inv => inv.Id == invoiceId);
        }
        public Invoice CreateInvoice(Invoice invoice)
        {
            _ctx.Invoices.Add(invoice);
            _ctx.SaveChanges();

            return invoice;
        }
        public Invoice UpdateInvoice(int invoiceId, Invoice invoice)
        {
            var existing = _ctx.Invoices.SingleOrDefault(inv => inv.Id == invoiceId);
            if (existing == null) return null;

            _ctx.Entry(existing).State = EntityState.Detached;
            invoice.Id = existing.Id;
            _ctx.Invoices.Update(invoice);
            _ctx.SaveChanges();

            return invoice;
        }
        public bool DeleteInvoice(int invoiceId)
        {
            var entity = _ctx.Invoices.Find(invoiceId);
            if (entity == null)
            {
                return false;
            }
            
            _ctx.Invoices.Remove(entity);
            _ctx.SaveChanges();
            
            return true;
        }
        #endregion

        #region InvoiceItem methods
        public List<InvoiceItem> GetInvoiceItems(int invoiceId)
        {
            return _ctx.InvoiceItems.Where(invoiceItem => invoiceItem.InvoiceId == invoiceId).ToList();
        }
        public InvoiceItem GetInvoiceItem(int invoiceId, int invoiceItemId)
        {
            return _ctx.InvoiceItems
                .Where(invoiceItem => invoiceItem.Id == invoiceItemId && invoiceItem.InvoiceId == invoiceId).FirstOrDefault();
        }
        public InvoiceItem CreateInvoiceItem(int invoiceId, InvoiceItem invoiceItem)
        {
            invoiceItem.InvoiceId = invoiceId;
            _ctx.InvoiceItems.Add(invoiceItem);
            _ctx.SaveChanges();

            return invoiceItem;
        }
        public InvoiceItem UpdateInvoiceItem(int invoiceItemId, InvoiceItem invoiceItem)
        {
            var existing = _ctx.InvoiceItems.SingleOrDefault(item => item.Id == invoiceItem.Id);
            if (existing == null) return null;

            invoiceItem.Id = existing.Id;
            _ctx.Entry(existing).State = EntityState.Detached;
            _ctx.InvoiceItems.Update(invoiceItem);
            _ctx.SaveChanges();

            return invoiceItem;

        }
        public bool DeleteInvoiceItem(int invoiceItemId)
        {
            var entity = _ctx.InvoiceItems.Find(invoiceItemId);
            if (entity == null)
            {
                return false;
            }

            _ctx.InvoiceItems.Remove(entity);
            _ctx.SaveChanges();
            
            return true;
        }
        #endregion
    }
}
