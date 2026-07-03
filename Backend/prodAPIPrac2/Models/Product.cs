using System.ComponentModel.DataAnnotations;

namespace prodAPIPrac2.Models
{
    public class Product
    {
       public int id { get; set; }
        [RegularExpression(@"^[a-zA-Z ]+$")]
        public  string? name { get; set; }
        public string? category { get; set; }
        public int? price { get; set; }
        public string? itemCode { get; set; }
        public int? quantity { get; set; }
        public string? brand { get; set; }
        public string? description { get; set; }
        public string? ImageUrl { get; set; }

    }
}
