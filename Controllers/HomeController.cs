using Microsoft.AspNetCore.Mvc;
namespace ProjetoLoja_API.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        
        public String Inicio()
        {
            return "Funcionou!";
        }

    }
}