using Newtonsoft.Json;

namespace PaymentAPI.Models;

/// <summary>
///     Modelo de esquema de produto
/// </summary>
public class Product
{
    public Product(string name)
    {
        Name = name;
    }

    /// <summary>
    ///     Nome e/ou descrição do produto
    /// </summary>
    /// <example>Produto 1</example>
    [JsonRequired]
    public string Name { get; set; }
}