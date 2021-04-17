using Microsoft.AspNetCore.Mvc;
using NexoApplication.Data;
using NexoApplication.Models.Services;
using System.Threading.Tasks;

namespace Nexo_Application.Controllers
{
    public class RankController : Controller
    {
        private readonly NexoDbContext _dbContext;
        private readonly ClienteService _clienteService;
        
        public RankController(NexoDbContext context, ClienteService clientService)
        {
            _dbContext = context;
            _clienteService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.SilverClients       = await _clienteService.GetSilverClients();
            ViewBag.GoldClients         = await _clienteService.GetGoldClients();
            ViewBag.PlatinumClients     = await _clienteService.GetPlatinumClients();
            ViewBag.DiamondClients      = await _clienteService.GetDiamondClients();

            return View();
        }
    }
}
