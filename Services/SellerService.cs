namespace PaymentAPI.Services
{
    using Models;
    using System.Reflection;

    public class SellerService
    {
        private readonly List<Seller> _sellerList = new();

        public Seller GetSeller(int id) => _sellerList.Find(s => s.Id == id);

        public void AddSeller(Seller seller) => _sellerList.Add(seller);

        public Seller UpdateSeller(Seller seller)
        {
            var currentSeller = GetSeller(seller.Id);

            foreach (var property in seller.GetType().GetProperties())
            {
                var newValue = property.GetValue(seller);

                if (newValue is "string" or null) continue;

                PropertyInfo propertyInfo = currentSeller.GetType().GetProperty(property.Name);

                propertyInfo?.SetValue(currentSeller, newValue, null);
            }

            return currentSeller;
        }
    }
}