using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PharmacyInventorySystem.Models
{
    public class Sale
    {
        
        public int SaleID { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Invoice ID must contain only numbers.")]
        public int InvoiceID { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Medicine name must contain only letters, numbers, and spaces.")]
        public string Medicine { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
        public int Quantity { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must be a valid decimal number with up to two decimal places.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public DateTime SaleDate { get; set; }
    }
}