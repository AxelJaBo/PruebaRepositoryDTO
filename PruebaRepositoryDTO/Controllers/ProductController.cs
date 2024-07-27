using Microsoft.AspNetCore.Mvc;
using PruebaRepositoryDTO.DTO;
using PruebaRepositoryDTO.Models;
using PruebaRepositoryDTO.Repository;

namespace PruebaRepositoryDTO.Controllers
{
	public class ProductController : Controller
	{
		private readonly ProductRepository _repo;
		private readonly ProviderRepository _providerRepo;
		private readonly ProductTypeRepository _productTypeRepository;

        public ProductController(ProductRepository repo, ProviderRepository providerRepo, ProductTypeRepository productTypeRepository)
        {
            _repo = repo;
            _providerRepo = providerRepo;
            _productTypeRepository = productTypeRepository;
        }

        public IActionResult Index()
		{
			var data = _repo.GetProducts().Select(p => new PruductDTO
			{
				Id = p.Id,
				Nombre = p.Nombre,
				IDProveedor = p.IDProveedor,
				IDTipo = p.IDTipo,
				Cantidad = p.Cantidad,
				FechaAlta = p.FechaAlta,
				Modelo = p.Modelo,
				Marca = p.Marca,
			}).ToList();
			return View(data);
		}

		public IActionResult Create()
		{
			var providers = _providerRepo.GetProviders().Select(p => new ProviderDTO
			{
				ID = p.ID,
				Nombre = p.Nombre
			}).ToList();			
			
			var productTypes = _productTypeRepository.GetProductTypes().Select(p => new ProductTypeDTO
			{
				ID = p.ID,
				Nombre = p.Nombre
			}).ToList();

			var model = new PruductDTO
			{
				Providers = providers,
                ProductTypes = productTypes
			};

			return View(model);
		}

		[HttpPost]
		public IActionResult Create(PruductDTO dto)
		{
			if (ModelState.IsValid)
			{
				var product = new Product()
				{
					Nombre = dto.Nombre,
					IDProveedor = dto.IDProveedor,
					IDTipo = dto.IDTipo,
					Cantidad = dto.Cantidad,
					Modelo = dto.Modelo,
					Marca = dto.Marca,
				};
				_repo.AddProduct(product);
				return RedirectToAction("Index");
			}

			dto.Providers = _providerRepo.GetProviders().Select(p => new ProviderDTO
			{
				ID = p.ID,
				Nombre = p.Nombre
			}).ToList();
			dto.ProductTypes = _productTypeRepository.GetProductTypes().Select(p => new ProductTypeDTO
			{
				ID = p.ID,
				Nombre = p.Nombre
			}).ToList();
			return View(dto);
		}
	}
}
