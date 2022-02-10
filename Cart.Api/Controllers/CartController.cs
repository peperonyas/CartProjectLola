using Cart.Core.Abstract.Service;
using Cart.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Cart.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CartController : BaseController
    {
        private readonly IAddService _addService;
        public CartController(IAddService addService)
        {
            _addService = addService;
        }
        [HttpPost]
        public async Task<ActionResult<AddRequestModel>> AddToCart(AddRequestModel parameters)
        {
            if (await _addService.AddToCart(parameters))
            {
                SetMessage("Product added to cart.");
                return parameters;
            }
            else
            {
                SetError("Not enough product");
                return Ok();
            }
        }
    }
}
