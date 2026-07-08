using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prodAPIPrac2.interfaces;

namespace prodAPIPrac2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController : ControllerBase
    {
        private readonly Iprodservice _service;

        public DashboardController(Iprodservice service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var data = await _service.GetDashboardData();

            return Ok(data);
        }
    }
}
