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
        public async Task<Product> Addpro(ProductDTO pro)
        {
            string imagePath = null;

            if (pro.Image != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(pro.Image.FileName);

                var folderPath = Path.Combine("wwwroot", "uploads");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await pro.Image.CopyToAsync(stream);
                }

                imagePath = "uploads/" + fileName;
            }

            var prod = new Product
            {
                name = pro.name,
                price = pro.price,
                category = pro.category,
                quantity = pro.quantity,
                itemCode = pro.itemCode,
                brand=pro.brand,
                description = pro.description,
                ImageUrl = imagePath
            };
            _context.products.Add(prod);
            await _context.SaveChangesAsync();
            return prod;
        }
        public async Task<Product> updatepro(int id, ProductDTO updatepro)
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
            if (updatepro.Image != null)
            {
                if (!string.IsNullOrEmpty(pro.ImageUrl))
                {
                    var oldPath = Path.Combine("wwwroot", pro.ImageUrl);

                    if (File.Exists(oldPath))
                    {
                        File.Delete(oldPath);
                    }
                }
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(updatepro.Image.FileName);
                var path = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await updatepro.Image.CopyToAsync(stream);
                }

                pro.ImageUrl = "uploads/" + fileName;
            }
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
