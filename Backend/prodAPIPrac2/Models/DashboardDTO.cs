namespace prodAPIPrac2.Models
{
    public class DashboardDTO
    {
        public int TotalProducts { get; set; }

        public decimal TotalInventoryValue { get; set; }

        public int LowStockProducts { get; set; }

        public int TotalCategories { get; set; }
        public List<CategoryCountDTO> ProductsByCategory { get; set; }
    }
}
