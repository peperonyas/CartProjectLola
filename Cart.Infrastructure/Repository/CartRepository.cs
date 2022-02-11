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
            using (var connection = CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>("checkquantityandinsert", new { id = parameters.ProductId, cnt =parameters.Quantity }, 
                    commandType:CommandType.StoredProcedure);
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
