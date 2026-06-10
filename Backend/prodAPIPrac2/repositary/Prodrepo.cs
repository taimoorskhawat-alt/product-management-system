using prodAPIPrac2.Models;
using prodAPIPrac2.data;
using Microsoft.EntityFrameworkCore;

namespace prodAPIPrac2.repositary
{
    public class Prodrepo : IProduct
    {
        private readonly appdbcontext _context;
        public Prodrepo(appdbcontext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> getpro()
        {
            return await _context.products.ToListAsync();
        }
        public async Task<Product?> getprobyid(int id)
        {
            return await _context.products.FindAsync(id);

        }
        public async Task<Product> Addpro(Product pro)
        {
            _context.products.Add(pro);
            await _context.SaveChangesAsync();
            return pro;
        }
        public async Task<Product?> updatepro(int id, Product updatepro)
        {
            var pro = await _context.products.FindAsync(id);
            if (pro == null)
            {
                return null;
            }
            pro.itemCode = updatepro.itemCode;
            pro.name = updatepro.name;
            pro.price = updatepro.price;
            pro.quantity = updatepro.quantity;
            pro.brand = updatepro.brand;
            pro.category = updatepro.category;
            pro.description = updatepro.description;

            await _context.SaveChangesAsync();
            return pro;
        }
        public async Task<bool> deletepro(int id)
        {
            var pro = await _context.products.FindAsync(id);
            if (pro == null)
            {
                return false;
            }
            _context.products.Remove(pro);
            await _context.SaveChangesAsync();
            return true;
        }
    }    
}
