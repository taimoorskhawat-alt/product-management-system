using prodAPIPrac2.Models;

namespace prodAPIPrac2.repositary
{
    public interface IProduct
    {
        Task<ProdPaginationDTO> getpro(int page, int pageSize, string sortColumn, bool sortAscending, string search, string category);
        Task<Product?> getprobyid(int id);
        Task<Product> Addpro(ProductDTO pro);
        Task<Product> updatepro(int id, ProductDTO updatepro);
        Task<bool> deletepro(int id);
    }
}
