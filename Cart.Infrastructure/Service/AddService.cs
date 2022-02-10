using Cart.Core.Abstract.Repository;
using Cart.Core.Abstract.Service;
using Cart.Core.Entities;

namespace Cart.Infrastructure.Service
{
    public class AddService : IAddService
    {
        private readonly ICartRepository _cartRepository;
        public AddService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> AddToCart(AddRequestModel parameters)
        {
            if (await _cartRepository.CheckQuantity(parameters))            
                return true;
            else
                return false;
        }
    }
}
