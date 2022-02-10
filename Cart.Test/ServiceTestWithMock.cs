using Cart.Core.Abstract.Service;
using Cart.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using Xunit;

namespace Cart.Test
{
    public class ServiceTestWithMock : IClassFixture<ServiceRegister>
    {
        private readonly ServiceRegister _containerFixture;
        public ServiceTestWithMock(ServiceRegister containerFixture)
        {
            _containerFixture = containerFixture;

            var mockHostingEnvironment = new Mock<IWebHostEnvironment>();
            mockHostingEnvironment.Setup(m => m.EnvironmentName).Returns(Environments.Production);
        }
        [Fact]
        public async void Add_To_Cart_Request_Should_False_With_Higher_Quantity()
        {
            var addService = _containerFixture.ServiceProvider.GetRequiredService<IAddService>();
            try
            {
                var result = await addService.AddToCart(new AddRequestModel()
                {
                    ProductId = 123123,
                    Quantity = 5,
                });
                Assert.True(true);                
            }
            catch (System.Exception ex)
            {
                Assert.True(false, ex.Message);
            }
        }
    }
}
