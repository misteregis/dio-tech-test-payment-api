namespace PaymentAPI.Services
{
    using Models;

    public class SaleService
    {
        private readonly List<Sale> _salesList = new();

        public List<Sale> GetSalesList()
        {
            return _salesList;
        }

        public Sale CreateSale(
            int orderId,
            int sellerId,
            string userCpf,
            string userName,
            string userEmail,
            string userPhoneNumber,
            List<Product> products
        )
        {
            Sale sale = new(orderId, new Seller(sellerId, userCpf, userName, userEmail, userPhoneNumber));

            AddProductsToSale(sale, products);

            AddSaleToSaleList(sale);

            return sale;
        }

        private void AddSaleToSaleList(Sale sale)
        {
            _salesList.Add(sale);
        }

        private static void AddProductsToSale(Sale sale, List<Product> products)
        {
            foreach (var product in products)
            {
                sale.AddProduct(new Product(product.Name));
            }
        }

        public Sale UpdateSaleStatus(Sale sale, EnumSaleStatus saleStatus)
        {
            List <EnumSaleStatus> pendingPayment = new()
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

            if (saleStatusList.ContainsKey(sale.Status))
            {
                if (saleStatusList[sale.Status].Contains(saleStatus))
                    sale.Status = saleStatusList[sale.Status].Find(x => x == saleStatus);
            }

            return sale;
        }
    }
}