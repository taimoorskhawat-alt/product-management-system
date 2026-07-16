using Microsoft.EntityFrameworkCore.Metadata.Internal;
using prodAPIPrac2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prodAPIPrac2.Order_models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAtPurchase { get; set; }

        // Navigation Properties
        public Orders Order { get; set; }

        public Product Product { get; set; }
    }
}
