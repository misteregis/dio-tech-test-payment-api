namespace PaymentAPI.Models
{
    using Microsoft.AspNetCore.Mvc;

    public class Seller
    {
        [FromQuery(Name = "userId")]
        public int Id { get; set; }

        public string Cpf { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
