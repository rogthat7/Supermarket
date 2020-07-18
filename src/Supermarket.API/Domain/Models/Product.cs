namespace Supermarket.API.Domain.Models
{
    public class Product
    {
        public int PId { get; set; }
        public string PName { get; set; }
        public string PBrand { get; set; }
        public short PQuantityInStock { get; set; }
        public EUnitOfMeasurement UnitOfMeasurement { get; set; }
        public decimal PPurchasePrice { get; set; }
        public decimal PSalesPrice { get; set; }        
        public int PSellerID { get; set; }
        public long PBarcode { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}