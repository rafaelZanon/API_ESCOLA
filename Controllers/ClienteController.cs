using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoLoja_API.Data;
using ProjetoLoja_API.Models;
namespace ProjetoLoja_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
        private readonly LojaContext _context;
        public ClienteController(LojaContext context)
        {
            // construtor
            _context = context;
        }

        [HttpGet]
public ActionResult<List<Cliente>> GetAll()
{
return _context.Cliente.ToList();
}

        [HttpPost]
        public async Task<ActionResult> post(Cliente model)
        {
            try
            {
                _context.Cliente.Add(model);
                if (await _context.SaveChangesAsync() == 1)
                {
                    //return Ok();
                    return Created($"/api/Cliente/{model.UserName}", model);
                }
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
            
            return BadRequest();
        }

        [HttpGet("{ClienteId}")]
        public ActionResult<List<Cliente>> Get(int ClienteId)
        {
            try
            {
                var result = _context.Cliente.Find(ClienteId);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
        [HttpDelete("{ClienteId}")]
        public async Task<ActionResult> delete(int ClienteId)
        {
            try
            {
                //verifica se existe Cliente a ser excluído
                var cliente = await _context.Cliente.FindAsync(ClienteId);
                if (cliente == null)
                {           
                    return NotFound();
                }
                _context.Remove(cliente);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }
        }
        [HttpPut("{ClienteId}")]
        public async Task<IActionResult> put(int ClienteId, Cliente dadosClienteAlt)
        {
            try
            {
                //verifica se existe Cliente a ser alterado
                var result = await _context.Cliente.FindAsync(ClienteId);
                if (ClienteId != result.id)
                {
                    return BadRequest();
                }
                result.UserName = dadosClienteAlt.UserName;
                result.RealName = dadosClienteAlt.RealName;
                result.Email = dadosClienteAlt.Email;
                await _context.SaveChangesAsync();
                return Created($"/api/Cliente/{dadosClienteAlt.UserName}", dadosClienteAlt);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Falha no acesso ao banco de dados.");
            }


        }
    }
}
