using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexoApplication.Data;
using NexoApplication.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexoApplication.Models.Services
{
    public class ProdutoService
    {
        private readonly NexoDbContext _dbContext;
    
        public ProdutoService(NexoDbContext context)
        {
            _dbContext = context;
        }

        public async Task<string> GetClientNameFromProduct(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);
            return cliente.Nome;
        }

        public async Task<IEnumerable<Produto>> GetProdutoByName(string productName)
        {
            string nomeProduto = productName.ToLower();

            IEnumerable<Produto> product = await _dbContext.Produtos.Where(p => p.Nome.ToLower() == nomeProduto).ToListAsync();
            
            return await Task.Run(() => product);
        }

        public IEnumerable<Produto> GetProdutoByClientId(int id)
        {
            var objProdutos = _dbContext.Produtos;
            IEnumerable<Produto> produtos = objProdutos.Where(prod => prod.ClienteId == id);
            return produtos;
        }
    }
}
