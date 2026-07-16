using Microsoft.EntityFrameworkCore.Metadata.Internal;
using prodAPIPrac2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace prodAPIPrac2.Order_models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = "Pending";

        // Navigation Properties
        public User User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
