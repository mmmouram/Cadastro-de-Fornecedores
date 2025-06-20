using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Services;
using MyApp.Application.DTOs.Request;
using MyApp.Application.DTOs.Response;
using MyApp.Domain.Exceptions;

namespace MyApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly IFornecedorService _service;
        public SupplierController(IFornecedorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdicionarFornecedorRequest request)
        {
            try
            {
                var response = await _service.AdicionarAsync(request);
                return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
            }
            catch (FornecedorException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AtualizarFornecedorRequest request)
        {
            try
            {
                var response = await _service.AtualizarAsync(id, request);
                return Ok(response);
            }
            catch (FornecedorException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var responses = await _service.ListarAsync();
            return Ok(responses);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var response = await _service.ObterPorIdAsync(id);
                return Ok(response);
            }
            catch (FornecedorException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}