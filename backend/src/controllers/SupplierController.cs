using Microsoft.AspNetCore.Mvc;
using Prudential.Backend.Models;
using Prudential.Backend.Services;
using System;
using System.Threading.Tasks;

namespace Prudential.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CriarFornecedor([FromBody] SupplierCadastroRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var fornecedor = await _service.CriarFornecedorAsync(request);
                return CreatedAtAction(nameof(ObterFornecedorPorId), new { id = fornecedor.Id }, fornecedor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFornecedor(int id, [FromBody] SupplierCadastroRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var fornecedor = await _service.AtualizarFornecedorAsync(id, request);
                return Ok(fornecedor);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterFornecedorPorId(int id)
        {
            var fornecedor = await _service.ObterFornecedorPorIdAsync(id);
            if (fornecedor == null)
                return NotFound();
            return Ok(fornecedor);
        }

        [HttpGet]
        public async Task<IActionResult> ListarFornecedores()
        {
            var fornecedores = await _service.ListarFornecedoresAsync();
            return Ok(fornecedores);
        }
    }
}
