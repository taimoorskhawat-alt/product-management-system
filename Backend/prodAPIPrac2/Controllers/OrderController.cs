using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prodAPIPrac2.interfaces;
using prodAPIPrac2.Order_models;
using System.Security.Claims;

namespace prodAPIPrac2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IorderService _orderService;
        public OrderController(IorderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PlaceOrder(PlaceOrderDTO dto)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var order = await _orderService.PlaceOrder(userId, dto);

            return Ok(order);
        }
        [HttpGet("my-orders")]
        [Authorize]
        public async Task<IActionResult> GetOrdersByUser()
        {
            var userId = int.Parse(
       User.FindFirst(ClaimTypes.NameIdentifier)!.Value
   );
            var orders=await _orderService.GetOrdersByUser(userId);

            return Ok(orders);
        }

    }
}
