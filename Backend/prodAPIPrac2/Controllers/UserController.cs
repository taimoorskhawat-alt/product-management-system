using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prodAPIPrac2.interfaces;
using System.Data;

namespace prodAPIPrac2.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IuserService _userService;

        public UserController(IuserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpPut("update-role/{id}")]
        public IActionResult UpdateRole(int id, string role)
        {
            var result = _userService.UpdateUserRole(id, role);
            if (!result) return NotFound();

            return Ok("Role updated successfully");
        }
    }
}
