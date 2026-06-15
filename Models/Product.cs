using System.ComponentModel.DataAnnotations;
using System;

namespace PharmacyInventorySystem.Models
{
    public class Product
    {
    
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Name must contain only letters, numbers, and spaces.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Category must contain only letters and spaces.")]
        public string Category { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Price must be a valid decimal number with up to two decimal places.")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative integer.")]
        public int Quantity { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }
    }
}