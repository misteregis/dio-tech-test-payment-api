namespace PaymentAPI.Services
{
    using Models;
    using Extensions;
    using System.Collections.Generic;

    /// <summary>
    /// Modelo de esquema de serviço de venda
    /// </summary>
    public class SaleService
    {
        /// <summary>
        /// Lista contendo as vendas
        /// </summary>
        private readonly List<Sale> _salesList = new();

        /// <summary>
        /// Obtém uma lista com as vendas efetuadas
        /// </summary>
        /// <returns>Lista de vendas</returns>
        public List<Sale> GetSalesList() => _salesList;

        /// <summary>
        /// Obtém uma venda passando o id
        /// </summary>
        /// <param name="id">ID do pedido/venda.</param>
        /// <returns>A venda.</returns>
        public Sale GetSale(int id) => _salesList.Find(s => s.Id == id);

        /// <summary>
        /// Adiciona uma nova venda à lista
        /// </summary>
        /// <param name="sale">A venda.</param>
        public void AddSale(Sale sale) => _salesList.Add(sale);

        /// <summary>
        /// Atualiza o status da venda
        /// </summary>
        /// <param name="sale">A venda.</param>
        /// <param name="saleStatus">O status.</param>
        /// <returns>Um objeto contendo a venda e uma mensagem de erro (caso exista).</returns>
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