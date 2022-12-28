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

        /// <summary>
        /// Registra uma nova venda
        /// </summary>
        /// <param name="sale">A venda.</param>
        /// <returns>A venda efetuda.</returns>
        /// <response code="201">Retorna a venda recém-criada.</response>
        /// <response code="400">Solicitação inválida.</response>
        [HttpPost, ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult RegisterSale(Sale sale)
        {
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

            return CreatedAtAction(nameof(RegisterSale), sale);
        }

        /// <summary>
        /// Obtém um pedido/venda passando um id
        /// </summary>
        /// <param name="orderId" example="1">ID do pedido.</param>
        /// <returns>A venda (se encontrado).</returns>
        /// <response code="200">Retorna a venda.</response>
        /// <response code="404">Venda não encontrada.</response>
        [HttpGet("{orderId:int}")]
        public IActionResult ObterPorId(int orderId)
        {
            var sale = _saleService.GetSale(orderId);

            if (sale == null)
                return NotFound();

            return Ok(sale);
        }

        /// <summary>
        /// Atualiza o status do pedido/venda
        /// </summary>
        /// <param name="orderId" example="1">ID do pedido/venda.</param>
        /// <param name="saleStatus">Status do pedido/venda.</param>
        /// <returns>A venda, em caso de falha irá retornar a mensagem com os erros.</returns>
        /// <response code="200">Retorna a venda ou uma mensagem de erro em caso de falha.</response>
        /// <response code="404">Venda não encontrada.</response>
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
