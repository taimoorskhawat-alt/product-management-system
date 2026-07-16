using prodAPIPrac2.Order_models;

namespace prodAPIPrac2.interfaces
{
    public interface IOrder
    {
        Task<OrderResponseDTO> PlaceOrder(int userId, PlaceOrderDTO dto);

        Task<List<OrderResponseDTO>> GetOrdersByUser(int userId);

        Task<List<OrderResponseDTO>> GetAllOrders();

        Task<OrderResponseDTO?> GetOrderById(int id);

        Task<bool> UpdateOrderStatus(int orderId, string status);
    }
}
