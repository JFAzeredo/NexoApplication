using Microsoft.EntityFrameworkCore;
using NexoApplication.Data;
using NexoApplication.Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexoApplication.Models.Services
{
    public class ClienteService
    {
        private readonly NexoDbContext _dbContext;
        private readonly ProdutoService _produtoService;

        public ClienteService(NexoDbContext context, ProdutoService produtoService)
        {
            _dbContext = context;
            _produtoService = produtoService;
        }

        public async Task<ICollection<Cliente>> GetAllClients()
        {
            return await _dbContext.Clientes.ToListAsync();
        }

        public async Task<ICollection<Cliente>> GetSilverClients()
        {
            DateTime timeToday = DateTime.Now;
            List<Cliente> clientes = await _dbContext.Clientes.Where(cliente => timeToday.Year - cliente.DataCadastro.Year >= 5).ToListAsync();
            return clientes;
        }

        public async Task<ICollection<Cliente>> GetGoldClients()
        {
            List<Cliente> clientes = await _dbContext.Clientes.ToListAsync();
            List<Cliente> goldClientes = new List<Cliente>();

            foreach(Cliente cliente in clientes)
            {
                List<Produto> produtos = await _dbContext.Produtos.Where(produto => produto.ClienteId == cliente.ClienteId).ToListAsync();
                if (produtos.Count >= 2 && produtos.Count <= 5)
                {
                    goldClientes.Add(cliente);
                }
            }
            return goldClientes;
        }

        public async Task<ICollection<Cliente>> GetPlatinumClients()
        {
            List<Cliente> clientes = await _dbContext.Clientes.ToListAsync();
            List<Cliente> platinumClientes = new List<Cliente>();

            foreach (Cliente cliente in clientes)
            {
                List<Produto> produtos = await _dbContext.Produtos.Where(produto => produto.ClienteId == cliente.ClienteId).ToListAsync();
                if (produtos.Count > 5 && produtos.Count <= 10)
                {
                    platinumClientes.Add(cliente);
                }
            }
            return platinumClientes;
        }

        public async Task<ICollection<Cliente>> GetDiamondClients()
        {
            List<Cliente> clientes = await _dbContext.Clientes.ToListAsync();
            List<Cliente> diamondClientes = new List<Cliente>();

            foreach (Cliente cliente in clientes)
            {
                List<Produto> produtos = await _dbContext.Produtos.Where(produto => produto.ClienteId == cliente.ClienteId).ToListAsync();
                if (produtos.Count > 10)
                {
                    diamondClientes.Add(cliente);
                }
            }
            return diamondClientes;
        }

        public async Task<ICollection<Cliente>> FindByMaster(string master)
        {
            ICollection<Cliente> clientes;

            if (master == "Prata")
            {
                clientes = await GetSilverClients();
            }
            else if (master == "Ouro")
            {
                clientes = await GetGoldClients();
            }
            else if (master == "Platina")
            {
                clientes = await GetPlatinumClients();
            }
            else
            {
                clientes = await GetDiamondClients();
            }

            return clientes;
        }

        public IEnumerable<Produto> GetProductById(int id) => _produtoService.GetProdutoByClientId(id);
    }
}
