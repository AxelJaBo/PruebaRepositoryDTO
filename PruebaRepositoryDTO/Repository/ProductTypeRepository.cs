using PruebaRepositoryDTO.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace PruebaRepositoryDTO.Repository
{
    public class ProductTypeRepository
    {
        private readonly string _connection;

        public ProductTypeRepository(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("conecction");
        }

        public IEnumerable<ProductType> GetProductTypes()
        {
            using (SqlConnection conecction = new(_connection))
            {
                using (SqlCommand command = new("SP_GetProductTypes", conecction))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    conecction.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<ProductType> productTypes = new List<ProductType>();
                        while (reader.Read())
                        {
                            ProductType productType = new ProductType()
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            };
                            productTypes.Add(productType);
                        }
                        return productTypes;
                    }
                }
            }
        }
    }
}
