using System.ComponentModel.DataAnnotations;
namespace PharmacyInventorySystem.Models
{
    public class Payment
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Supplier name must contain only letters and spaces.")]
        public string Supplier { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must be a valid decimal number with up to two decimal places.")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public string Status { get; set; }
    }
}