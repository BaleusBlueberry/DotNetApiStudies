using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Image
    {
        [Required(ErrorMessage = "Url is required")]
        [Range(14, int.MaxValue, ErrorMessage = "Url Link must be more then 14 characters")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Alt is required")]
        [Range(2, 256, ErrorMessage = "Alt must be between 2 and 256 characters")]
        public string Alt { get; set; }
    }
}
