using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using myApp.Models;
using myApp.Services;

namespace myApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        // CENÁRIO 1: Registro de fornecedor
        [HttpPost]
        public async Task<ActionResult<Fornecedor>> RegistrarFornecedor([FromBody] Fornecedor fornecedor)
        {
            var fornecedorCriado = await _fornecedorService.RegistrarFornecedor(fornecedor);
            return CreatedAtAction(nameof(ConsultarFornecedorPorId), new { id = fornecedorCriado.Id }, fornecedorCriado);
        }

        // CENÁRIO 2: Validação de dados do fornecedor
        [HttpPost("{id}/validar")]
        public async Task<ActionResult<Fornecedor>> ValidarFornecedor(int id)
        {
            var fornecedorValidado = await _fornecedorService.ValidarFornecedor(id);
            if (fornecedorValidado == null)
                return NotFound();
            return Ok(fornecedorValidado);
        }

        // CENÁRIO 4: Atualização dos dados do fornecedor
        [HttpPut("{id}")]
        public async Task<ActionResult<Fornecedor>> AtualizarFornecedor(int id, [FromBody] Fornecedor fornecedor)
        {
            var fornecedorAtualizado = await _fornecedorService.AtualizarFornecedor(id, fornecedor);
            if (fornecedorAtualizado == null)
                return NotFound();
            return Ok(fornecedorAtualizado);
        }

        // CENÁRIO 3: Consulta de fornecedor pelo ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> ConsultarFornecedorPorId(int id)
        {
            var fornecedor = await _fornecedorService.ConsultarFornecedorPorId(id);
            if (fornecedor == null)
                return NotFound();
            return Ok(fornecedor);
        }

        // CENÁRIO 3: Consulta com filtro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> ConsultarFornecedores([FromQuery] string filtro)
        {
            var fornecedores = await _fornecedorService.ConsultarFornecedores(filtro);
            return Ok(fornecedores);
        }
    }
}
