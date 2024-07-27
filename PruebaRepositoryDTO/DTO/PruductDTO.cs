using System;
using System.Collections.Generic;

namespace PruebaRepositoryDTO.DTO
{
	public class PruductDTO
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IDProveedor { get; set; }
		public int IDTipo { get; set; }
		public int Cantidad { get; set; }
		public DateTime FechaAlta { get; set; }
		public string Modelo { get; set; }
		public string Marca { get; set; }
		public List<ProviderDTO> Providers { get; set; } = new List<ProviderDTO>();
		public List<ProductTypeDTO> ProductTypes { get; set; } = new List<ProductTypeDTO>();
	}
}
