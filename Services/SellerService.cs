namespace PaymentAPI.Services
{
    using Models;

    public class SellerService
    {
        private readonly List<Seller> _sellerList = new();

        public Seller GetSeller(int id) => _sellerList.Find(s => s.Id == id);

        public List<Seller> GetSellersList() => _sellerList;

        public void CreateSeller(Seller seller) => _sellerList.Add(seller);
    }
}