using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Reminder.Models;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace Reminder.Data.Repositories;
public class EventoRepository
{
    private readonly ReminderDbContext _context;
    public EventoRepository(ReminderDbContext context)
    {
        _context = context;
    }

    public async Task<List<Evento>> BuscarEventosAsync()
    {
        return await _context.Eventos.AsNoTracking().ToListAsync();
    }

    public async Task<IAsyncEnumerable<dynamic>> BuscarEventosHojeAsync(string nomeUsuario, string nomeEvento, DateTime? dataEvento)
    {

        var query = _context.Eventos
            .Join(_context.EventosxUsuarios,
            ev => ev.Id,
            evu => evu.IdEvento,
            (ev, evu) => new { e = ev, eu = evu })
            .Join(_context.Usuarios,
            evx => evx.eu.IdUsuario,
            usu => usu.Id,
            (evx, usu) => new { EVU = evx, USU = usu })
             .Select(t => new { nomeUsuario = t.USU.Nome, nomeEvento = t.EVU.e.Titulo, dataEvento = t.EVU.e.Criacao, exclusivo = t.EVU.e.Exclusivo })
            .AsQueryable();
        if (nomeUsuario != null && nomeUsuario != "")
        {
            query = query.Where(a => a.nomeUsuario.Contains(nomeUsuario));
        }
        if (nomeEvento != null && nomeEvento != "")
        {
            query = query.Where(a => a.nomeEvento.Contains(nomeEvento));
        }
        if(dataEvento != null)
        {
            query = query.Where(a => a.dataEvento == dataEvento);
        }
        
            

        return query.AsAsyncEnumerable();

    }
    public async Task<Evento?> BuscarEventoPeloIdAsync(int id)
    {
        return await _context.Eventos.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }
    public async Task<int> CriarAsync(Evento evento)
    {
        var eventoSalvo = await _context.Eventos.AddAsync(evento);
        await _context.SaveChangesAsync();
        return eventoSalvo.Entity.Id;
    }
    public async Task EditarAsync(Evento evento)
    {
        _context.Eventos.Update(evento);
        await _context.SaveChangesAsync();
    }
    public async Task DeletarAsync(Evento evento)
    {
        _context.Eventos.Remove(evento);
        await _context.SaveChangesAsync();
    }
}