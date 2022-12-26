namespace PaymentAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Models;
    using Services;
    using System.ComponentModel.DataAnnotations;

    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class SaleController : ControllerBase
    {
        private readonly SaleService _saleService;
        private readonly SellerService _sellerService;

        public SaleController(IOptions<SellerService> selleServiceOptions, IOptions<SaleService> saleServiceOptions)
        {
            _sellerService = selleServiceOptions.Value;
            _saleService = saleServiceOptions.Value;
        }
    }
}
