namespace PaymentAPI.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Sale
    {
        [JsonProperty(PropertyName = "orderId", Order = -2)]
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnumSaleStatus Status;

        public DateTime SaleDate { get; set; }

        public Seller Seller { get; set; }

        public List<Product> Products { get; set; } = new();

        public Sale(int id, Seller seller)
        {
            Id = id;
            SaleDate = DateTime.Now;
            Seller = seller;
        }

        public void AddProduct(Product product) => Products.Add(product);
    }
}
