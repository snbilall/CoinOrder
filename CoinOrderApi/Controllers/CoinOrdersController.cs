using CoinOrderApi.Data.Models;
using CoinOrderApi.Exceptions;
using CoinOrderApi.Providers;
using CoinOrderApp.DtoModels.Request;
using CoinOrderApp.DtoModels.Response;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest requestDto)
        {
            if (requestDto == null) return BadRequest();

            await coinOrderProvider.CreateAsync(requestDto);

            return Created("", new { });
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> Get([FromRoute] int userId)
        {
            CoinOrder? order = await coinOrderProvider.GetAsync(userId);
            
            if (order == null) return NotFound();

            return Ok(new GetOrderResponse().ConvertFromEntity(order));
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] int userId)
        {
            try
            {
                await coinOrderProvider.DeleteAsync(userId);
            }
            catch (UserHasNoActiveOrderException)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("{orderId}/communication-permissions")]
        public async Task<IActionResult> CommunicationPermissions([FromRoute] Guid orderId)
        {
            try
            {
                return Ok(await coinOrderProvider.GetCommunicationPermissionsAsync(orderId));
            }
            catch (UserHasNoActiveOrderException)
            {
                return BadRequest();
            }
        }
    }
}
