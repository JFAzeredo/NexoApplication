using Microsoft.AspNetCore.Mvc;
using NexoApplication.Data;
using NexoApplication.Models.Entities;
using NexoApplication.Models.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NexoApplication.Controllers
{
    public class ClienteController : Controller
    {
        private readonly NexoDbContext _dbContext;
        private readonly ClienteService _clienteService;
        private readonly ProdutoService _produtoService;
        public ClienteController(NexoDbContext context, ClienteService clienteService, ProdutoService produtoService)
        {
            _dbContext = context;
            _clienteService = clienteService;
            _produtoService = produtoService;
        }

        List<Produto> produtos { get; set; } = new List<Produto>();
        public IActionResult Index()
        {
            IEnumerable<Cliente> clientes = _dbContext.Clientes;
            return View(clientes);
        }

        public IActionResult Create()
        {
            ViewBag.dateCriacao = DateTime.Now;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                DateTime dataCriacao = DateTime.Now;
                cliente.DataCadastro = dataCriacao;
                await _dbContext.Clientes.AddAsync(cliente);
                await _dbContext.SaveChangesAsync();
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => View(cliente));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);

            if (cliente == null) return NotFound();

            return await Task.Run(() => View(cliente));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var client = await _dbContext.Clientes.FindAsync(cliente.ClienteId);
                client.Nome = cliente.Nome;
                client.SobreNome = cliente.SobreNome;
                client.Email = cliente.Email;
                client.Ativo = cliente.Ativo;
                _dbContext.Update(client);
                await _dbContext.SaveChangesAsync();
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => View(cliente));
        }



        public async Task<IActionResult> Detail(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            ViewBag.Produtos = _produtoService.GetProdutoByClientId(cliente.ClienteId).ToList();
            return await Task.Run(() => View(cliente));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _dbContext.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return await Task.Run(() => View(cliente));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = _dbContext.Clientes.Find(id);
            if (cliente != null)
            {
                _dbContext.Clientes.Remove(cliente);
                await _dbContext.SaveChangesAsync();
                return await Task.Run(() => RedirectToAction("Index"));
            } else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Busca(string master)
        {
            ICollection<Cliente> clientes = await _clienteService.FindByMaster(master);
            return View(clientes);
        }
    }
}
