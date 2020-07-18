namespace Supermarket.API.Domain.Models.Queries
{
    public class SellersQuery : Query
    {
        public int? SId { get; set; }

        public SellersQuery(int? SellerId, int page, int itemsPerPage) : base(page, itemsPerPage)
        {
            SId = SellerId;
        }
    }
}