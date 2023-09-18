namespace Reminder.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Email { get; set; }

    public string? Senha { get; set; }

    public List<Evento> Eventos { get; set; } = new List<Evento>();
}
