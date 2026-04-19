using CibertecDemo.Data;
using CibertecDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace CibertecDemo.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoRepository prodRepo;
        
        public ProductoController(ProductoRepository prodRepo)
        {
            this.prodRepo = prodRepo;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductoModel prod)
        {
            if (!ModelState.IsValid)
            {
                return View(prod);
            }

            await prodRepo.AgregarProductoAsync(prod);

            return RedirectToAction(nameof(Index));
        }

        /*
                public async Task<IActionResult> Index()
                {
                    var prod = await prodRepo.ObtenerProductosAsync();
                    return View(prod);
                }
        */

        public async Task<IActionResult> Index(int page = 1) 
        {
            int elementosPorPagina = 5;
            var (listaProductos, totalRegistros) = await prodRepo.obtenerProductosPaginado(page, elementosPorPagina);
            ViewBag.PaginaActual = page;
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)totalRegistros / elementosPorPagina);
            return View(listaProductos);
        }
    }
}
