using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myApp.Models;
using myApp.Context;

namespace myApp.Repositories
{
    public class FornecedorRepository : IFornecedorRepository
    {
        private readonly FornecedorDbContext _context;

        public FornecedorRepository(FornecedorDbContext context)
        {
            _context = context;
        }

        public async Task<Fornecedor> AdicionarFornecedor(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor;
        }

        public async Task CriarHistorico(Fornecedor fornecedor)
        {
            var historico = new FornecedorHistorico
            {
                FornecedorId = fornecedor.Id,
                Nome = fornecedor.Nome,
                Documento = fornecedor.Documento,
                TipoFornecedor = fornecedor.TipoFornecedor,
                Status = fornecedor.Status,
                Versao = fornecedor.Versao,
                DataAlteracao = fornecedor.DataCriacao
            };
            _context.FornecedorHistoricos.Add(historico);
            await _context.SaveChangesAsync();
        }

        public async Task<Fornecedor> AtualizarFornecedor(Fornecedor fornecedor)
        {
            _context.Fornecedores.Update(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor;
        }

        public async Task<Fornecedor> ObterFornecedorPorId(int id)
        {
            return await _context.Fornecedores.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Fornecedor>> ObterFornecedores(string filtro)
        {
            var query = _context.Fornecedores.AsQueryable();
            if (!string.IsNullOrEmpty(filtro))
            {
                query = query.Where(f => f.Nome.Contains(filtro) || f.Documento.Contains(filtro));
            }
            return await query.ToListAsync();
        }
    }
}
