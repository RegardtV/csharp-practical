using FullStack.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullStack.ViewModels;


namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController: ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public IActionResult GetAllInvoice()
        {
            var invoices = _invoiceService.GetAll();
            
            return Ok(invoices);
        }

        [HttpGet("{invoiceId}", Name = "GetInvoice")]
        public IActionResult GetInvoice (int invoiceId)
        {
            var invoiceToReturn = _invoiceService.GetById(invoiceId);
            if (invoiceToReturn == null)
            {
                return NotFound();
            }

            return Ok(invoiceToReturn);
        }

        [HttpPost]
        public IActionResult CreateInvoice(InvoiceForManipulationModel invoice)
        {
            var invoiceToReturn = _invoiceService.Create(invoice);

            return CreatedAtRoute("GetInvoice",
                new { invoiceId = invoiceToReturn.Id },
                invoiceToReturn);
        }

        [HttpPut("{invoiceId}")]
        public IActionResult UpdateInvoice(int invoiceId, InvoiceForManipulationModel invoice)
        {
            var invoiceToReturn = _invoiceService.Update(invoiceId, invoice);
            if (invoiceToReturn == null)
            {
                return NotFound();
            }
            return Ok(invoiceToReturn);
        }

        [HttpDelete("{invoiceId}")]
        public IActionResult DeleteInvoice(int invoiceId)
        {
            var success = _invoiceService.Delete(invoiceId);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
