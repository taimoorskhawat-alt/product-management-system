using prodAPIPrac2.Models;
namespace prodAPIPrac2.interfaces
{
    public interface Iprodservice
    {
        Task<IEnumerable<Product>> getpro();
        Task<Product?> getprobyid(int id);
        Task<Product> Addpro(Product pro);
        Task<Product> updatepro(int id, Product updatepro);
        Task<bool> deletepro(int id);
    }
}
