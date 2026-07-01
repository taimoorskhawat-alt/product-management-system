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
        public async Task<ProdPaginationDTO> getpro(int page, int pageSize, string sortColumn, bool sortAscending, string search, string category)
        {
            var query = _context.products.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x =>
                    x.name.Contains(search) 
                    
                    
                  );
            }
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(x =>
       x.category.ToLower() == category.ToLower());
            }
            // SORTING
            if (!string.IsNullOrEmpty(sortColumn))
            {
                if (sortColumn == "name")
                    query = sortAscending ? query.OrderBy(x => x.name)
                                          : query.OrderByDescending(x => x.name);

                if (sortColumn == "price")
                    query = sortAscending ? query.OrderBy(x => x.price)
                                          : query.OrderByDescending(x => x.price);

                if (sortColumn == "quantity")
                    query = sortAscending ? query.OrderBy(x => x.quantity)
                                          : query.OrderByDescending(x => x.quantity);
            }

            // PAGINATION
            var totalCount = await query.CountAsync();

            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new ProdPaginationDTO
            {
                Products = products,
                TotalCount = totalCount
            };
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
