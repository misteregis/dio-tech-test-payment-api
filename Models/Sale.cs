using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PaymentAPI.Models;

/// <summary>
///     Modelo de esquema de venda
/// </summary>
public class Sale
{
    [JsonPropertyName("orderId")] [JsonProperty("orderId")]
    public int Id;

    public DateTime SaleDate = DateTime.Now;

    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public EnumSaleStatus Status;

    [Required(ErrorMessage = "RequiredSeller")]
    [Newtonsoft.Json.JsonRequired]
    public Seller Seller { get; set; }

    [Required(ErrorMessage = "RequiredProduct")]
    [MinLength(1, ErrorMessage = "RequiredProduct")]
    [Newtonsoft.Json.JsonRequired]
    public List<Product> Products { get; set; } = null!;
}