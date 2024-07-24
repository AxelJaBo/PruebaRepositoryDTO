using Microsoft.AspNetCore.Mvc;
using PruebaRepositoryDTO.DTO;
using PruebaRepositoryDTO.Models;
using PruebaRepositoryDTO.Repository;

namespace PruebaRepositoryDTO.Controllers
{
	public class ProductController : Controller
	{
		private readonly ProductRepository _repo;

		public ProductController(ProductRepository repo)
		{
			_repo = repo;
		}

		public IActionResult Index()
		{
			var data = _repo.GetProducts().Select(p=> new PruductDTO
			{
				Id = p.Id,
				Nombre = p.Nombre,
				IDProveedor=p.IDProveedor,
				IDTipo=p.IDTipo,
				Cantidad=p.Cantidad,
				Modelo=p.Modelo,
				Marca=p.Marca,
			}).ToList();
			return View(data);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(PruductDTO dto)
		{
			if (ModelState.IsValid)
			{
				var product = new Product()
				{
					Nombre=dto.Nombre,
					IDProveedor = dto.IDProveedor,
					IDTipo = dto.IDTipo,
					Cantidad = dto.Cantidad,
					Modelo = dto.Modelo,
					Marca = dto.Marca
				};
				_repo.AddProduct(product);
				return RedirectToAction("Index");
			}
			return View(dto);
		}
	}
}
