using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Address
    {
        public string? State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public required string Country { get; set; }

        [Required(ErrorMessage = "City is required")]
        public required string City { get; set; }

        [Required(ErrorMessage = "Street is required")]
        public required string Street { get; set; }

        [Required(ErrorMessage = "HouseNumber is required")]
        [Range(1, int.MaxValue, ErrorMessage = "house number must be positive")]
        public int HouseNumber { get; set; }

        
        public int Zip { get; set; } = 0;
    }
}
