using System.Data;
using System.Data.SqlClient;
using PruebaRepositoryDTO.Models;

namespace PruebaRepositoryDTO.Repository
{
    public class ProductReportRepository
    {
        private readonly string _connection;

        public ProductReportRepository(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("conecction");
        }

        public IEnumerable<ProductReport> GetProductsReport()
        {
            using(SqlConnection connection = new(_connection))
            {
                using(SqlCommand command = new("SP_GetProductsReport", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        List<ProductReport> productsreport = new List<ProductReport>();
                        while (reader.Read())
                        {
                            ProductReport productreport = new ProductReport()
                            {
                                IDProducto = reader.GetInt32(reader.GetOrdinal("IDProducto")),
                                NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                                Marca = reader.GetString(reader.GetOrdinal("Marca")),
                                Modelo = reader.GetString(reader.GetOrdinal("Modelo")),
								TipoProducto = reader.GetString(reader.GetOrdinal("TipoProducto")),
								Proveedor = reader.GetString(reader.GetOrdinal("Proveedor")),
                                Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad"))
                            };
                            productsreport.Add(productreport);
                        }
                        return productsreport;
                    }
                    
                }
            }
        }
    }
}
