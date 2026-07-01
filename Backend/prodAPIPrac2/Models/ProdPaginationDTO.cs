namespace prodAPIPrac2.Models
{
    public class ProdPaginationDTO
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        public int TotalCount { get; set; }

    }
}
