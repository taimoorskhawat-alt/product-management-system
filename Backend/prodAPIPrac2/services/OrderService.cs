using prodAPIPrac2.interfaces;
using prodAPIPrac2.Order_models;

namespace prodAPIPrac2.services
{
    public class OrderService:IorderService
    {
        private readonly IOrder _orderRepo;
        public OrderService(IOrder orderRepo)
        {
            _orderRepo = orderRepo;
        }
        public async Task<OrderResponseDTO> PlaceOrder(int userId, PlaceOrderDTO dto)
        {
            return await _orderRepo.PlaceOrder(userId, dto);
        }
        public async Task<List<OrderResponseDTO>> GetOrdersByUser(int userId)
        {
            return await _orderRepo.GetOrdersByUser(userId);
        }
       
    }
}
