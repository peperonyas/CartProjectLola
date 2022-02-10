using Cart.Core.Abstract.Service;
using Cart.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Cart.Test
{
    public class ServiceTestWithoutMock : ServiceRegister
    {
        private readonly ServiceRegister _serviceRegister;
        public ServiceTestWithoutMock(ServiceRegister serviceRegister)
        {
            _serviceRegister = serviceRegister;
        }
        [Fact]
        public async void ServTest()
        {
            var service = _serviceRegister.ServiceProvider.GetRequiredService<IAddService>();
            try
            {
                var request = new AddRequestModel
                {
                    ProductId = 123123,
                    Quantity = 12,
                };
                var response = await service.AddToCart(request);
                Assert.False(response);
            }
            catch (System.Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }
    }
}
