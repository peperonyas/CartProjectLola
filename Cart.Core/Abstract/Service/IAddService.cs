using Cart.Core.Entities;

namespace Cart.Core.Abstract.Service
{
    public interface IAddService
    {
        Task<bool> AddToCart(AddRequestModel parameters);
    }
}
