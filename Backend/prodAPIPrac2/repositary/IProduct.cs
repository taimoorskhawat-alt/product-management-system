using prodAPIPrac2.Models;

namespace prodAPIPrac2.repositary
{
    public interface IProduct
    {
        Task<IEnumerable<Product>> getpro();
        Task<Product?> getprobyid(int id);
        Task<Product> Addpro(Product pro);
        Task<Product> updatepro(int id, Product updatepro);
        Task<bool> deletepro(int id);
    }
}
