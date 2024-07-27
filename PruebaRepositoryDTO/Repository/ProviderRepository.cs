using PruebaRepositoryDTO.Models;
using System.Data.SqlClient;
using System.Data;

namespace PruebaRepositoryDTO.Repository
{
	public class ProviderRepository
	{
		private readonly string _connection;

		public ProviderRepository(IConfiguration configuration)
		{
			_connection = configuration.GetConnectionString("conecction");
		}

		public IEnumerable<Provider> GetProviders()
		{
			using (SqlConnection conecction = new(_connection))
			{
				using (SqlCommand command = new("SP_GetProviders", conecction))
				{
					command.CommandType = CommandType.StoredProcedure;
					conecction.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						List<Provider> providers = new List<Provider>();
						while (reader.Read())
						{
							Provider provider = new Provider()
							{
								ID = reader.GetInt32(reader.GetOrdinal("ID")),
								Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
							};
							providers.Add(provider);
						}
						return providers;
					}
				}
			}
		}
	}
}
