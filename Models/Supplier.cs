using System.ComponentModel.DataAnnotations;
namespace PharmacyInventorySystem.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters and spaces.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\+?[\d\s\-]+$", ErrorMessage = "Only numbers, spaces and + are allowed")]
        public string Contact { get; set; }
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only numbers are allowed")]
        public string Medicine { get; set; }
        [Required]
        public DateTime LastOrder { get; set; }

        public string Status { get; set; }
    }
}