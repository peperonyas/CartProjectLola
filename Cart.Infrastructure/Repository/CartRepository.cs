using Cart.Core.Abstract.Repository;
using Cart.Core.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Cart.Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        protected readonly IConfiguration _configuration;
        public CartRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> CheckQuantity(AddRequestModel parameters)
        {
            string query = $" IF EXISTS(SELECT 1 FROM Products WHERE ProductId = {parameters.ProductId} and Quantity >= {parameters.Quantity}) THEN " +
                $" INSERT INTO Cart(ProductId, Quantity) VALUES({parameters.ProductId}, {parameters.Quantity}); " +
                $" SELECT  1;      " +
                $" END IF; " +
                $" SELECT 0; ";
            using (var connection = CreateConnection())
            {
                return await connection.ExecuteAsync(query) == 1 ? true : false;
                //return await connection.ExecuteScalarAsync<bool>("CheckQuantityAndInsert", new { parameters.ProductId, parameters.Quantity });
            }
        }
        protected IDbConnection CreateConnection()
        {
            var conn = SqlConnection();
            conn.Open();
            return conn;
        }
        private IDbConnection SqlConnection()
        {
            return new Npgsql.NpgsqlConnection(_configuration.GetConnectionString("CartDb"));
        }
    }
}
