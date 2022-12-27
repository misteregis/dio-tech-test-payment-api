namespace PaymentAPI.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Text.Json.Serialization;

    public class Sale
    {
        [JsonPropertyName("orderId"), JsonProperty(PropertyName = "orderId", Order = -2)]
        public int Id;

        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public EnumSaleStatus Status;

        public DateTime SaleDate = DateTime.Now;

        public Seller Seller { get; set; }

        public List<Product> Products { get; set; }
    }
}
