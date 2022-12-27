namespace PaymentAPI.Services
{
    using Models;
    using Extensions;
    using System.Collections.Generic;

    public class SaleService
    {
        private readonly List<Sale> _salesList = new();

        public List<Sale> GetSalesList() => _salesList;

        public Sale GetSale(int id) => _salesList.Find(s => s.Id == id);

        public void AddSale(Sale sale) => _salesList.Add(sale);

        public object[] UpdateSaleStatus(Sale sale, EnumSaleStatus saleStatus)
        {
            object[] response = { sale, null };

            List<EnumSaleStatus> pendingPayment = new()
            {
                EnumSaleStatus.ApprovedPayment,
                EnumSaleStatus.Canceled
            };

            List<EnumSaleStatus> approvedPayment = new()
            {
                EnumSaleStatus.SentToCarrier,
                EnumSaleStatus.Canceled
            };

            List<EnumSaleStatus> sentToCarrier = new() { EnumSaleStatus.Delivered };

            Dictionary<EnumSaleStatus, List<EnumSaleStatus>> saleStatusList = new()
            {
                { EnumSaleStatus.PendingPayment, pendingPayment },
                { EnumSaleStatus.ApprovedPayment, approvedPayment },
                { EnumSaleStatus.SentToCarrier, sentToCarrier }
            };

            var message = sale.Status switch
            {
                EnumSaleStatus.Canceled => new { message = "Este pedido foi cancelado!" },
                EnumSaleStatus.Delivered => new { message = "Este pedido já foi entregue!" },
                _ => new { message = "Não houve alteração!" }
            };

            if (!saleStatusList.ContainsKey(sale.Status))
                return new object[] { sale, message };

            var allowed = saleStatusList[sale.Status];

            if (saleStatusList[sale.Status].Contains(saleStatus))
                sale.Status = allowed.Find(x => x == saleStatus);
            else
            {
                var messages = new List<string>();

                allowed.ForEach(x => messages.Add(x.GetEnumMemberValue()));

                response[1] = new { message = $"Desculpe, este pedido só pode ser alterado para \"{string.Join("\" ou \"", messages)}\"" };
            }

            return response;
        }
    }
}