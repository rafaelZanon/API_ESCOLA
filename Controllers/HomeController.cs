using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjetoLoja_API.Data;
using ProjetoLoja_API.Models;

namespace ProjetoLoja_API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly LojaContext? _context;
        public HomeController(

        IConfiguration configuration,
        LojaContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        
        public ActionResult<dynamic> Login([FromBody] Cliente cliente)
        {

            var conta = _context.Cliente.Where(u => u.email == cliente.email &&

            u.senha == cliente.senha)

            .FirstOrDefault();


            if (conta == null)
                return Unauthorized("Usuário ou senha inválidos");
            var authClaims = new List<Claim> {
            new Claim(ClaimTypes.Name, conta.userName),
            new Claim(ClaimTypes.Role, conta.role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
};
            var token = GetToken(authClaims);
            conta.senha = "";
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                user = conta
            });
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}",
        User.Identity.Name);


       
        [HttpGet]
        [Route("Admin")]
        [Authorize(Roles = "Admin")]
        public bool admin() => true;

      
        [HttpGet]
        [Route("Cliente")]
        [Authorize(Roles = "Cliente")]
        public bool cliente() => true;

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new

            SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
            
            expires: DateTime.Now.AddHours(3),
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey,

            SecurityAlgorithms.HmacSha256)

            );
            return token;
        }
    }
}