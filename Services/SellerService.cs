using PaymentAPI.Models;

namespace PaymentAPI.Services;

/// <summary>
///     Modelo de esquema de serviço do vendedor
/// </summary>
public class SellerService
{
    /// <summary>
    ///     Lista contendo os vendedores
    /// </summary>
    private readonly List<Seller> _sellerList = new();

    /// <summary>
    ///     Obtém um vendedor passando o id
    /// </summary>
    /// <param name="id">O id do vendedor.</param>
    /// <returns>O vendedor.</returns>
    public Seller GetSeller(int id)
    {
        return _sellerList.Find(s => s.Id == id);
    }

    /// <summary>
    ///     Adiciona um novo vendedor à lista
    /// </summary>
    /// <param name="seller">O vendedor.</param>
    public void AddSeller(Seller seller)
    {
        _sellerList.Add(seller);
    }

    /// <summary>
    ///     Atualiza o status da venda (segundo a regra).
    /// </summary>
    /// <param name="seller">O vendedor.</param>
    /// <returns>O vendedor.</returns>
    public Seller UpdateSeller(Seller seller)
    {
        var currentSeller = GetSeller(seller.Id);

        foreach (var property in seller.GetType().GetProperties())
        {
            var newValue = property.GetValue(seller);

            if (newValue is "string" or null) continue;

            var propertyInfo = currentSeller.GetType().GetProperty(property.Name);

            propertyInfo?.SetValue(currentSeller, newValue, null);
        }

        return currentSeller;
    }
}