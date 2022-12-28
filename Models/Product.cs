namespace PaymentAPI.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Modelo de esquema de produto
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Nome e/ou descrição do produto
        /// </summary>
        /// <example>Produto 1</example>
        [JsonRequired]
        public string Name { get; set; }

        public Product(string name)
        {
            Name = name;
        }
    }
}
