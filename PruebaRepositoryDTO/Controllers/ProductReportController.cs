using Microsoft.AspNetCore.Mvc;
using PruebaRepositoryDTO.DTO;
using PruebaRepositoryDTO.Repository;

namespace PruebaRepositoryDTO.Controllers
{
    public class ProductReportController : Controller
    {
        private readonly ProductReportRepository _repo;

        public ProductReportController(ProductReportRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            var data = _repo.GetProductsReport().Select(p=> new ProductoReportDTO
            {
                IDProducto=p.IDProducto,
                NombreProducto=p.NombreProducto,
                Marca=p.Marca,
                Modelo=p.Modelo,
                TipoProducto=p.TipoProducto,
                Proveedor=p.Proveedor,
                Cantidad=p.Cantidad
            }).ToList();
            return View(data);
        }
    }
}
