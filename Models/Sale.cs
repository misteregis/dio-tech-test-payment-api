namespace PaymentAPI.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Modelo de esquema de venda
    /// </summary>
    public class Sale
    {
        [JsonPropertyName("orderId"), JsonProperty("orderId")]
        public int Id;

        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public EnumSaleStatus Status;

        public DateTime SaleDate = DateTime.Now;

        [Required(ErrorMessage = "RequiredSeller")]
        [Newtonsoft.Json.JsonRequired]
        public Seller Seller { get; set; }

        [Required(ErrorMessage = "RequiredProduct"), MinLength(1, ErrorMessage = "RequiredProduct")]
        [Newtonsoft.Json.JsonRequired]
        public List<Product> Products { get; set; } = null!;
    }
}
