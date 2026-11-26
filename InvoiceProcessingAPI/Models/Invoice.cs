using System.ComponentModel.DataAnnotations;

namespace InvoiceProcessingAPI.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }


        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; } = string.Empty;


        [Required]
        [StringLength(150)]
        public string VendorName { get; set; } = string.Empty;

        public DateTime InvoiceDate { get; set; }


        [Range(typeof(decimal),"0.01", "9999999999999999.9999")]
        public decimal Amount { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";

        public string? RejectionReason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
