using Microsoft.EntityFrameworkCore;
using prodAPIPrac2.data;
using prodAPIPrac2.interfaces;
using prodAPIPrac2.Order_models;

namespace prodAPIPrac2.repositary
{
    public class OrderRepo : IOrder
    {
        private readonly appdbcontext _context;
        public OrderRepo(appdbcontext context)
        {
            _context = context;
        }

        public Task<List<OrderResponseDTO>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<OrderResponseDTO?> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderResponseDTO>> GetOrdersByUser(int userId)
        {
            var orders= await _context.Orders
        .Where(x => x.UserId == userId)
        .Include(x => x.OrderItems)
        .ToListAsync();
            return orders.Select(order => new OrderResponseDTO
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,

                OrderItems = order.OrderItems.Select(item => new OrderItemResponseDTO
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    PriceAtPurchase = item.PriceAtPurchase
                }).ToList()

            }).ToList();
        }

        public async Task<OrderResponseDTO> PlaceOrder(int userId, PlaceOrderDTO dto)
        {
            if (dto.Items == null || !dto.Items.Any())
            {
                throw new Exception("Order must contain at least one product.");
            }
            using var transaction = await _context.Database.BeginTransactionAsync();
            decimal totalAmount = 0;
            try
            {

                var orderItems = new List<OrderItem>();
                foreach (var item in dto.Items)
                {
                    var product = await _context.products
           .FirstOrDefaultAsync(x => x.id == item.ProductId);

                    if (product == null)
                    {
                        throw new Exception($"Product {item.ProductId} not found.");
                    }
                    if (product.quantity < item.Quantity)
                    {
                        throw new Exception($"Not enough stock for product {product.name}.");
                    }
                    var itemTotal = (decimal)product.price * item.Quantity;

                    totalAmount += itemTotal;
                    var orderItem = new OrderItem
                    {
                        ProductId = product.id,
                        Quantity = item.Quantity,
                        PriceAtPurchase = (decimal)product.price
                    };

                    orderItems.Add(orderItem);
                    product.quantity -= item.Quantity;
                }

                var order = new Orders
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    Status = "Pending"
                };

                order.OrderItems = orderItems;
                _context.Orders.Add(order);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new OrderResponseDTO
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    Status = order.Status,

                    OrderItems = order.OrderItems.Select(x => new OrderItemResponseDTO
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        PriceAtPurchase = x.PriceAtPurchase
                    }).ToList()
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public Task<bool> UpdateOrderStatus(int orderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
