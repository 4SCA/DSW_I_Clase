using Microsoft.AspNetCore.Mvc;
using CibertecDemo.Data;
using CibertecDemo.Models;

namespace CibertecDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoApiController : ControllerBase
    {
        private readonly ProductoRepository prodRepo;

        public ProductoApiController(ProductoRepository prodRepo) 
        {
            this.prodRepo = prodRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoModel>>> Get() 
        {
            var productos = await prodRepo.ObtenerProductosAsync();
            return Ok(productos);
        }
    }
}
