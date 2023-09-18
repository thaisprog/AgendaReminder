using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reminder.Data;
using Reminder.Data.Repositories;
using Reminder.Models;
using Reminder.Services;

namespace Reminder.Controllers
{
    public class EventosController : Controller
    {
        private readonly EventoService _eventoService;
        private readonly EventoRepository _eventoRepository;
        private readonly UsuarioRepository _usuarioRepository;


        public EventosController(EventoService eventoService, EventoRepository eventoRepository, UsuarioRepository usuarioRepository)
        {
            _eventoService = eventoService;
            _eventoRepository = eventoRepository;
            _usuarioRepository = usuarioRepository;

        }

        public async Task<IActionResult> Index()
        {
            return View(await _eventoRepository.BuscarEventosAsync());
        }

        public async Task<IActionResult> AdicionarUsuario(int id)
        {
            var usuario = await _usuarioRepository.BuscarUsuarioAsyncPeloId(id);
            if (usuario is Usuario)
                ViewBag.UsuariosSelecionados.Add(usuario);

            return RedirectToAction(nameof(Create));
        }

        public async Task<IActionResult> Details(int id)
        {
            var evento = await _eventoRepository.BuscarEventoPeloIdAsync(id);
            if (evento is null)
            {
                return NotFound();
            }
            return View(evento);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await _usuarioRepository.BuscarUsuariosAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Evento evento)
        {
            if (!ModelState.IsValid)
                return View(evento);

            int.TryParse(Request.Form["usuario_selecionado"], out int idUsuarioSelecionado);

            if (idUsuarioSelecionado != 0) await _eventoService.CriarEventoComUsuariosAsync(evento, new int[] { idUsuarioSelecionado });

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var evento = await _eventoRepository.BuscarEventoPeloIdAsync(id);

            if (evento == null) return NotFound();

            return View(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [FromForm] Evento evento)
        {
            if (!ModelState.IsValid)
                return View(evento);

            if (id != evento.Id)
                evento.Id = id;

            await _eventoRepository.EditarAsync(evento);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var evento = await _eventoRepository.BuscarEventoPeloIdAsync(id);
            if (evento is null)
                return Problem("Não foi possível deletar");

            await _eventoRepository.DeletarAsync(evento);

            return RedirectToAction(nameof(Index));
        }
    }
}