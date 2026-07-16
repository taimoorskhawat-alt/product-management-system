namespace prodAPIPrac2.Order_models
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = string.Empty;

        public List<OrderItemResponseDTO> OrderItems { get; set; } = new();
    }
}
