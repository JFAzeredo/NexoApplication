using Microsoft.AspNetCore.Mvc;
using NexoApplication.Data;
using NexoApplication.Models.Entities;
using NexoApplication.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NexoApplication.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly NexoDbContext _dbContext;
        private readonly ProdutoService _produtoService;

        public ProdutoController(NexoDbContext context, ProdutoService produtoService)
        {
            _dbContext = context;
            _produtoService = produtoService;
        }


        public IActionResult Index()
        {
            IEnumerable<Produto> produtos = _dbContext.Produtos;
            return View(produtos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.Valor <= 0)
                {
                    throw new Exception();
                }
                await _dbContext.Produtos.AddAsync(produto);
                await _dbContext.SaveChangesAsync();
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => View(produto));
        }


        public async Task<IActionResult> Edit(int id)
        {
            var produto = await _dbContext.Produtos.FindAsync(id);

            if (produto == null) return NotFound();

            return await Task.Run(() => View(produto));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Produtos.Update(produto);
                await _dbContext.SaveChangesAsync();
                return await Task.Run(() => RedirectToAction("Index"));
            }
            return await Task.Run(() => View(produto));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _dbContext.Produtos.FindAsync(id);
            ViewBag.ClientName = await _produtoService.GetClientNameFromProduct(produto.ClienteId);
            if (produto == null) return NotFound();

            return await Task.Run(() => View(produto));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            var produto = await _dbContext.Produtos.FindAsync(id);
            if (produto != null)
            {
                _dbContext.Produtos.Remove(produto);
                await _dbContext.SaveChangesAsync();
                return await Task.Run(() => RedirectToAction("Index"));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Busca(string productName)
        {
            var produto = await _produtoService.GetProdutoByName(productName);
            return View(produto);
        }
    }
}
