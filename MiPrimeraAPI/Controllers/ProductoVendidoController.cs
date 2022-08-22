using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Controllers.DTOS;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {

        [HttpGet]
        public List<ProductoVendido> GetProductosVendidos()
        {
            return ProductoVendidoHandler.GetProductosVendidos();
        }
    }
}
