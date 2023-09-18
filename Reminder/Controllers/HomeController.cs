using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Abstractions;
using Reminder.Data.Repositories;
using Reminder.Models;
using System.Diagnostics;

namespace Reminder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EventoRepository _eventoRepository;

        public HomeController(ILogger<HomeController> logger, EventoRepository eventoRepository)
        {
            _logger = logger;
            _eventoRepository = eventoRepository;
        }

        public IActionResult Index()
        {
            ViewBag.Eventos = null;
            return View();
        }

        public async Task<IActionResult> Hoje()
        {
            string nomeUsuario = Request.Form["nomeUsuario"].ToString();
            string nomeEvento = Request.Form["nomeEvento"].ToString();
            DateTime? dataEvento = Request.Form["dataEvento"].ToString() != null && Request.Form["dataEvento"].ToString() != "" ? Convert.ToDateTime(Request.Form["dataEvento"]) : null;


            ViewBag.Eventos = await _eventoRepository.BuscarEventosHojeAsync(nomeUsuario, nomeEvento, dataEvento);
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}