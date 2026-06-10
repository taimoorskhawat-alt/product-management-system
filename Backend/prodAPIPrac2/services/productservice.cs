using prodAPIPrac2.interfaces;
using prodAPIPrac2.Models;
using prodAPIPrac2.repositary;

namespace prodAPIPrac2.Services
{
    public class ProductService : Iprodservice
    {
        private readonly IProduct _repo;

        public ProductService(IProduct repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Product>> getpro()
        {
            return await _repo.getpro();
        }

        public async Task<Product?> getprobyid(int id)
        {
            return await _repo.getprobyid(id);
        }

        public async Task<Product> Addpro(Product pro)
        {
            return await _repo.Addpro(pro);
        }

        public async Task<Product?> updatepro(int id, Product updatepro)
        {
            return await _repo.updatepro(id, updatepro);
        }

        public async Task<bool> deletepro(int id)
        {
            return await _repo.deletepro(id);
        }
    }
}