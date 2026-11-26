
using Microsoft.EntityFrameworkCore;
using InvoiceProcessingAPI.Models;

namespace InvoiceProcessingAPI.Data
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(
            DbContextOptions<InvoiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
    }
}
