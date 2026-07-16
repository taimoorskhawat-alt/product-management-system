using prodAPIPrac2.Order_models;

namespace prodAPIPrac2.interfaces
{
    public interface IorderService
    {
        Task<OrderResponseDTO> PlaceOrder(int userId, PlaceOrderDTO dto);
        Task<List<OrderResponseDTO>> GetOrdersByUser(int userId);
    }
}
