using System.Runtime.Serialization;

namespace PaymentAPI.Models;

/// <summary>
///     Modelo de esquema de status
/// </summary>
public enum EnumSaleStatus
{
    /// <summary>
    ///     Aguardando pagamento
    /// </summary>
    [EnumMember(Value = "Aguardando pagamento")]
    PendingPayment,

    /// <summary>
    ///     Pagamento aprovado
    /// </summary>
    [EnumMember(Value = "Pagamento aprovado")]
    ApprovedPayment,

    /// <summary>
    ///     Enviado para transportadora
    /// </summary>
    [EnumMember(Value = "Enviado para transportadora")]
    SentToCarrier,

    /// <summary>
    ///     Entregue
    /// </summary>
    [EnumMember(Value = "Entregue")] Delivered,

    /// <summary>
    ///     Cancelada
    /// </summary>
    [EnumMember(Value = "Cancelada")] Canceled
}