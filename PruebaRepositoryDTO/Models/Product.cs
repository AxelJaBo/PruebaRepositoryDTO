namespace PruebaRepositoryDTO.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public int IDProveedor {  get; set; }
		public int IDTipo { get; set; }
		public int Cantidad { get; set; }
		public DateTime FechaAlta { get; set; }
		public string Modelo {  get; set; }
		public string Marca { get; set; }
	}
}
