
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InvoiceProcessingAPI.Data;
using InvoiceProcessingAPI.Models;

namespace InvoiceProcessingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly InvoiceDbContext _context;

        public InvoicesController(InvoiceDbContext context)
        {
            _context = context;
        }

        // GET: api/invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }

        // GET: api/invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
                return NotFound();

            return invoice;
        }

        // POST: api/invoices
        [HttpPost]
        public async Task<ActionResult<Invoice>> CreateInvoice(Invoice invoice)
        {
            if (invoice.Amount <= 0)
                return BadRequest("Invoice amount must be greater than zero.");

            var duplicate = await _context.Invoices
                .AnyAsync(i => i.InvoiceNumber == invoice.InvoiceNumber);

            if (duplicate)
                return Conflict("Invoice number already exists.");

            invoice.InvoiceId = 0;
            invoice.Status = "Pending";
            invoice.CreatedAt = DateTime.UtcNow;

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetInvoice),
                new { id = invoice.InvoiceId },
                invoice
            );
        }

        // PUT: api/invoices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, Invoice invoice)
        {
            if (id != invoice.InvoiceId)
                return BadRequest("Invoice ID mismatch.");

            var existingInvoice = await _context.Invoices.FindAsync(id);

            if (existingInvoice == null)
                return NotFound();

            var duplicate = await _context.Invoices.AnyAsync(
                i => i.InvoiceNumber == invoice.InvoiceNumber &&
                     i.InvoiceId != id
            );

            if (duplicate)
                return Conflict("Invoice number already exists.");

            existingInvoice.InvoiceNumber = invoice.InvoiceNumber;
            existingInvoice.VendorName = invoice.VendorName;
            existingInvoice.InvoiceDate = invoice.InvoiceDate;
            existingInvoice.Amount = invoice.Amount;
            existingInvoice.Status = invoice.Status;
            existingInvoice.RejectionReason = invoice.RejectionReason;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
                return NotFound();

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
