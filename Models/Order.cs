
using System.ComponentModel.DataAnnotations;

namespace PharmacyInventorySystem.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Supplier name must contain only letters and spaces.")]
        public string Supplier { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Items must be a positive integer.")]
        public int Items { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}