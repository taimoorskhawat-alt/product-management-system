using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prodAPIPrac2.Models;
using Microsoft.AspNetCore.Authorization;
using prodAPIPrac2.interfaces;
using System.Data;


namespace prodAPIPrac2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdController : ControllerBase
    {
       
        private readonly Iprodservice _serv;
        public ProdController(Iprodservice serv)
        {
            _serv = serv;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<ProdPaginationDTO>> getpro( 
            int page = 1, int pageSize = 10, string sortColumn = "",  bool sortAscending = true, string search = "", string category = "")
        {
           
            var pro = await _serv.getpro(page, pageSize,sortColumn, sortAscending, search,category);
            
            return Ok(pro);
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>>getprobyid(int id)
        {
            var pro =await _serv.getprobyid(id);
                if (pro == null)
            {
                return NotFound();
            }
            return pro;
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Product>> Addpro(Product pro)
        {
            var newpro = await _serv.Addpro(pro);
            return Ok(newpro);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>>updatepro(int id,Product updatepro)
        {
            var pro = await _serv.updatepro(id,updatepro);
            if (pro == null)
            {
                return NotFound();
            }
           
            return Ok(pro);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> deletepro(int id)
        {
            var pro = await _serv.deletepro(id);
            if (!pro)
            {
                return NotFound(new {message="Product Not Found of this id"});
            }
          
            return Ok(new {message="product Deleted Successfully"});
        }

    }   
}
