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

        [HttpPost("RegisterSale")]
        public IActionResult RegisterSale([FromQuery] Seller seller, [Required] List<Product> products)
        {
            if (products.Count == 0)
                return BadRequest(new { message = "Por favor, adicione ao menos um produto!" });

            var sales = _saleService.GetSalesList();
            var orderId = sales.Any() ? (sales.Max(sale => sale.Id) + 1) : 1;
            var currentSeller = _sellerService.GetSellersList().Find(x => x.Id == seller.Id);
            var sellerId = seller.Id > 0 ? seller.Id : 1;

            if (currentSeller != null)
            {
                currentSeller.Cpf = seller.Cpf ?? currentSeller.Cpf;
                currentSeller.Name = seller.Name ?? currentSeller.Name;
                currentSeller.Email = seller.Email ?? currentSeller.Email;
                currentSeller.PhoneNumber = seller.PhoneNumber ?? currentSeller.PhoneNumber;
            }
            else
            {
                seller.Id = sellerId;
                currentSeller = seller;

                _sellerService.CreateSeller(currentSeller);
            }

            var sale = _saleService.CreateSale(orderId, currentSeller, products);

            return CreatedAtAction(nameof(RegisterSale), null, sale);
        }

        [HttpGet("GetSaleById")]
        [ProducesResponseType(typeof(Sale), StatusCodes.Status200OK)]
        public IActionResult GetSaleById(int id)
        {
            var sale = _saleService.GetSalesList().Find(s => s.Id == id);

            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        [HttpPost("UpdateSaleStatus")]
        public IActionResult UpdateSaleStatus([Required] int orderId, [Required] EnumSaleStatus saleStatus)
        {
            var sale = _saleService.GetSalesList().Find(x => x.Id == orderId);

            if (sale == null)
                return NotFound();

            var oldSaleStatus = sale.Status;

            if (oldSaleStatus == EnumSaleStatus.Canceled)
                return Ok(new { message = "Este pedido foi cancelado!" });

            if (oldSaleStatus == EnumSaleStatus.Delivered)
                return Ok(new { message = "Este pedido já foi entregue!" });

            _saleService.UpdateSaleStatus(sale, saleStatus);

            return sale.Status == oldSaleStatus ? Ok(new { message = "Não houve alteração!" }) : Ok(sale);
        }

        [HttpGet("{orderId:int}")]
        public IActionResult ObterPorId(int orderId)
        {
            var sale = _saleService.GetSalesList().Find(x => x.Id == orderId);

            if (sale == null)
                return NotFound();

            return Ok(sale);
        }
    }
}
