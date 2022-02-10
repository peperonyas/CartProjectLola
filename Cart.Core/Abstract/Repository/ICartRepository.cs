using Cart.Core.Entities;

namespace Cart.Core.Abstract.Repository
{
    public interface ICartRepository
    {
        Task<bool> CheckQuantity(AddRequestModel parameters);
    }
}
