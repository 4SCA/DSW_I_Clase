using Microsoft.AspNetCore.Mvc;
using CibertecDemo.Data;

namespace CibertecDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoApiController : ControllerBase
    {
        readonly ProductoRepository prodRepo;

        public ProductoApiController(ProductoRepository productoRepository) 
        {
            this.prodRepo = productoRepository;
        }
    }
}
