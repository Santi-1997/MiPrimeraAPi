using Microsoft.AspNetCore.Mvc;
using MiPrimeraAPI.Model;
using MiPrimeraAPI.Repository;

namespace MiPrimeraAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NombreController : ControllerBase
    {
        [HttpGet]
        public string TraerNombre()
        {
            return "Mi Primera Api";
        }
    }
}
