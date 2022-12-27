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

        [HttpPost]
        public IActionResult RegisterSale([Required] Sale sale)
        {
            if (sale.Seller == null)
                return BadRequest(new { message = "É obrigatório um vendedor!" });

            if (!sale.Products.Any())
                return BadRequest(new { message = "É obrigatório ter ao menos um produto!" });

            var sales = _saleService.GetSalesList();

            var orderId = sales.Any() ? (sales.Max(x => x.Id) + 1) : 1;
            var sellerId = sale.Seller.Id > 0 ? sale.Seller.Id : 1;
            var currentSeller = _sellerService.GetSeller(sellerId);

            sale.Id = orderId;
            sale.Seller.Id = sellerId;

            if (currentSeller != null)
                sale.Seller = _sellerService.UpdateSeller(sale.Seller);
            else
                _sellerService.AddSeller(sale.Seller);

            _saleService.AddSale(sale);

            return Ok(sale);
        }

        [HttpGet("{orderId:int}")]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
        public IActionResult ObterPorId(int orderId)
        {
            var sale = _saleService.GetSale(orderId);

            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        [HttpPost("UpdateSaleStatus")]
        public IActionResult UpdateSaleStatus([Required] int orderId, [Required] EnumSaleStatus saleStatus)
        {
            var sale = _saleService.GetSale(orderId);

            if (sale == null)
                return NotFound();

            var response = _saleService.UpdateSaleStatus(sale, saleStatus);

            return Ok(response.Last() ?? response.First());
        }
    }
}
