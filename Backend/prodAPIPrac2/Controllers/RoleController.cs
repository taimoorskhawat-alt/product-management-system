using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prodAPIPrac2.data;
using prodAPIPrac2.Models;
using System.Data;

namespace prodAPIPrac2.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly appdbcontext _context;

        public RoleController(appdbcontext context)
        {
            _context = context;
        }

        // GET ALL ROLES
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }

        // ADD ROLE
        [HttpPost]
        public async Task<IActionResult> AddRole(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return Ok(role);
        }

        // DELETE ROLE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return NotFound();
            var isUsed = _context.users.Any(u => u.Role == role.Name);

            if (isUsed)
            {
                return BadRequest("Role is assigned to users");
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, Role updatedRole)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role == null)
                return NotFound();

            role.Name = updatedRole.Name;

            await _context.SaveChangesAsync();

            return Ok(role);
        }
    }
}
