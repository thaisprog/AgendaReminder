using System;
using System.Collections.Generic;

namespace Reminder.Models;

public partial class Evento
{
    public int Id { get; set; }

    public string? Titulo { get; set; }

    public string? Descricao { get; set; }

    public DateTime Criacao { get; set; }

    public TimeSpan Horario { get; set; }

    public bool Exclusivo { get; set; }

    public List<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
