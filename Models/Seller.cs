namespace PaymentAPI.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Modelo de esquema do vendedor
    /// </summary>
    public class Seller
    {
        /// <example>1</example>
        [Required(ErrorMessage = "RequiredField")]
        public int Id { get; set; }

        /// <example>012.345.678-90</example>
        [Required(ErrorMessage = "RequiredField")]
        public string Cpf { get; set; }

        /// <example>Misteregis</example>
        [Required(ErrorMessage = "RequiredField")]
        public string Name { get; set; }

        /// <example>misteregis@gmail.com</example>
        [EmailAddress(ErrorMessage = "InvalidMail")]
        public string Email { get; set; }

        /// <example>(21) 98765-4321</example>
        [Phone(ErrorMessage = "InvalidPhone")]
        public string PhoneNumber { get; set; }
    }
}
