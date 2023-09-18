using Microsoft.EntityFrameworkCore;
using Reminder.Models;

namespace Reminder.Data.Repositories
{
    public class UsuarioRepository
    {
        private readonly ReminderDbContext _context;
        public UsuarioRepository(ReminderDbContext context)
        {
            _context = context;
        }
        public async Task<List<Usuario>> BuscarUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }
        public async Task<Usuario> BuscarUsuarioAsyncPeloId(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}