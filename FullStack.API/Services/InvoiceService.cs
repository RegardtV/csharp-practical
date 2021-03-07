using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using FullStack.API.Helpers;
using FullStack.ViewModels;
using FullStack.Data.Entities;
using FullStack.Data;
using FullStack.API.Exceptions;

namespace FullStack.API.Services
{
    public interface IInvoiceService
    {
        IEnumerable<InvoiceModel> GetAll();
        InvoiceModel GetById(int id);
        InvoiceModel Create(InvoiceForManipulationModel invoice);
        InvoiceModel Update(int invoiceId, InvoiceForManipulationModel invoice);
        bool Delete(int id);
    }

    public class InvoiceService: IInvoiceService
    {
        private readonly IFullStackRepository _repo;
        private readonly IInvoiceValidator _validator;
        private readonly AppSettings _appSettings;
        public InvoiceService(IFullStackRepository repo, IInvoiceValidator validator, IOptions<AppSettings> appSettings)
        {
            this._repo = repo;
            this._validator = validator;
            this._appSettings = appSettings.Value;
        }

        public IEnumerable<InvoiceModel> GetAll()
        {
            var invoiceList = _repo.GetInvoices();

            return invoiceList.Select(inv => MapToInvoiceModel(inv));
        }

        public InvoiceModel GetById(int id)
        {
            var invoiceEntity = _repo.GetInvoice(id);
            if (invoiceEntity == null) return null;

            return MapToInvoiceModel(invoiceEntity);
        }

        public InvoiceModel Create(InvoiceForManipulationModel invoice)
        {
            var results = _validator.Validate(invoice).ToArray();
            if (results.Length > 0)
                throw new ValidationException(results);

            Invoice invoiceEntity = MapToInvoiceEntity(invoice);
            invoiceEntity = _repo.CreateInvoice(invoiceEntity);       
            return MapToInvoiceModel(invoiceEntity);
        }

        public InvoiceModel Update(int invoiceId, InvoiceForManipulationModel invoice)
        {
            var results = _validator.Validate(invoice).ToArray();
            if (results.Length > 0)
                throw new ValidationException(results);

            var invoiceEntity = MapToInvoiceEntity(invoice);
            invoiceEntity = _repo.UpdateInvoice(invoiceId, invoiceEntity);
            if (invoiceEntity == null) return null;

            return MapToInvoiceModel(invoiceEntity);
        }

        public bool Delete(int id)
        {
            return _repo.DeleteInvoice(id);
        }

        // helper methods
        private InvoiceModel MapToInvoiceModel(Invoice invoice)
        {
            ICollection<InvoiceItemModel> list = new List<InvoiceItemModel>();
            foreach(InvoiceItem item in invoice.Items)
            {
                list.Add(MapToInvoiceItemModel(item));
            }
            return new InvoiceModel
            {
                Id = invoice.Id,
                RefNumber = invoice.RefNumber,
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
                Total = invoice.Total,
                Items = list
            };
        }

        private Invoice MapToInvoiceEntity(InvoiceForManipulationModel invoice)
        {
            return new Invoice (getRefNumber())
            {
                IssueDate = invoice.IssueDate,
                DueDate = invoice.DueDate,
            };
        }
        private InvoiceItemModel MapToInvoiceItemModel(InvoiceItem item)
        {
            return new InvoiceItemModel
            {
                Id = item.Id,
                ServiceDate = item.ServiceDate,
                ServiceDescription = item.ServiceDescription,
                ServiceRate = item.ServiceRate,
                ServiceHours = item.ServiceHours,
                ServiceCost = item.ServiceCost,
                InvoiceId = item.InvoiceId
            };
        }

        private string getRefNumber()
        {
            const int lengthNumberPart = 4;
            string ticks = DateTime.Now.Ticks.ToString();
            return $"INV{ticks.Substring(ticks.Length - lengthNumberPart)}";
        }
    }
}
