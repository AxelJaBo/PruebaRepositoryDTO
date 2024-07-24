using PruebaRepositoryDTO.Models;
using System.Data.SqlClient;
using System.Data;

namespace PruebaRepositoryDTO.Repository
{
	public class ProductRepository
	{
		private readonly string _connection;

		public ProductRepository(IConfiguration configuration)
		{
			_connection = configuration.GetConnectionString("conecction");
		}

		public IEnumerable<Product> GetProducts()
		{
			using (SqlConnection connection = new(_connection))
			{
				using (SqlCommand command = new("SP_GetProducts", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						List<Product> products = new List<Product>();
						while (reader.Read())
						{
							Product product = new Product()
							{
								Id = reader.GetInt32(reader.GetOrdinal("Id")),
								Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
								IDProveedor = reader.GetInt32(reader.GetOrdinal("IDProveedor")),
								IDTipo = reader.GetInt32(reader.GetOrdinal("IDTipo")),
								Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
								FechaAlta = reader.GetDateTime(reader.GetOrdinal("FechaAlta")),
								Modelo = reader.GetString(reader.GetOrdinal("Modelo")),
								Marca = reader.GetString(reader.GetOrdinal("Marca"))
							};
							products.Add(product);
						}
						return products;
					}

				}
			}
		}

		public void AddProduct(Product product)
		{
			using (SqlConnection connection = new(_connection))
			{
				using (SqlCommand command = new("SP_AddProduct", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@Nombre", product.Nombre);
					command.Parameters.AddWithValue("@IDProveedor", product.IDProveedor);
					command.Parameters.AddWithValue("@IDTipo", product.IDTipo);
					command.Parameters.AddWithValue("@Cantidad", product.Cantidad);
					command.Parameters.AddWithValue("@Modelo", product.Modelo);
					command.Parameters.AddWithValue("@Marca", product.Marca);
					connection.Open();
					command.ExecuteNonQuery();
				}
			}
		}
	}
}
