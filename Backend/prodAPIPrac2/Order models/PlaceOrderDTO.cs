using System.ComponentModel.DataAnnotations;

namespace prodAPIPrac2.Order_models
{
    public class PlaceOrderDTO
    {
        [Required]
        public List<OrderItemDTO> Items { get; set; } = new();
    }
}
