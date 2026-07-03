using System.ComponentModel.DataAnnotations;

namespace prodAPIPrac2.Models
{
    public class ProductDTO
    {
        [Required]
        public string name { get; set; }

        [Required]
        public int price { get; set; }

        [Required]
        public string category { get; set; }

        [Required]
        public int quantity { get; set; }

        [Required]
        public string itemCode { get; set; }

        public string brand { get; set; }

        public string? description { get; set; }

        public IFormFile? Image { get; set; }

    }
}
