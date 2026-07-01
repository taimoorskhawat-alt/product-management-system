using prodAPIPrac2.Models;
namespace prodAPIPrac2.interfaces
{
    public interface Iprodservice
    {
        Task<ProdPaginationDTO> getpro(int page, int pageSize, string sortColumn, bool sortAscending, string search, string category);
        Task<Product?> getprobyid(int id);
        Task<Product> Addpro(Product pro);
        Task<Product> updatepro(int id, Product updatepro);
        Task<bool> deletepro(int id);
    }
}
