using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Controllers.DTOS;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet]
        public List<Venta> GetVentas()
        {
            return VentaHandler.GetVentas();
        }
    }
}
