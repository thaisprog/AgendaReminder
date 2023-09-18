using Reminder.Data;
using Reminder.Data.Repositories;
using Reminder.Models;

namespace Reminder.Services
{
    public class EventoService
    {
        private readonly EventoRepository _eventoRepository;
        private readonly ReminderDbContext _context;
        public EventoService(EventoRepository eventoRepository, ReminderDbContext context)
        {
            _eventoRepository = eventoRepository;
            _context = context;
        }

        public async Task CriarEventoComUsuariosAsync(Evento evento, int[] idsUsuariosSelecionados)
        {
            try
            {
                int idEventoCriado = await _eventoRepository.CriarAsync(evento);
                await _eventoRepository.BuscarEventoPeloIdAsync(idEventoCriado);

                foreach (int id in idsUsuariosSelecionados)
                {
                    var responsavel = new EventosxUsuarios
                    {
                        IdEvento = idEventoCriado,
                        IdUsuario = id
                    };
                    await _context.EventosxUsuarios.AddAsync(responsavel);
                }
                await _context.SaveChangesAsync();
            }
            catch
            {
            }
        }
    }
}
