using CoinOrderApi.Providers;
using CoinOrderApp.DtoModels.Request;
using Microsoft.AspNetCore.Mvc;

namespace CoinOrderApi.Controllers
{
    [ApiController]
    [Route("coin-orders")]
    public class CoinOrdersController : Controller
    {
        private CoinOrderProvider coinOrderProvider;
        public CoinOrdersController(CoinOrderProvider coinOrderProvider)
        {
            this.coinOrderProvider = coinOrderProvider;
        }

        [HttpPost(Name = "coin-orders")]
        public async Task<IActionResult> Create([FromBody] CreatePurchaseRequest requestDto)
        {
            if (requestDto == null) return BadRequest();

            await coinOrderProvider.CreateAsync(requestDto);

            return Created("", new { });
        }
    }
}
