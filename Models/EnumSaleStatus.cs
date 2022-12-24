namespace PaymentAPI.Models
{
    using System.Runtime.Serialization;

    public enum EnumSaleStatus
    {
        [EnumMember(Value = "Aguardando pagamento")]
        PendingPayment,
        [EnumMember(Value = "Pagamento aprovado")]
        ApprovedPayment,
        [EnumMember(Value = "Enviado para transportadora")]
        SentToCarrier,
        [EnumMember(Value = "Entregue")]
        Delivered,
        [EnumMember(Value = "Cancelada")]
        Canceled
    }
}
