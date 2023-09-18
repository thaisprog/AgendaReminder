using System;
using System.Collections.Generic;

namespace Reminder.Models;

public partial class EventosxUsuarios
{
    public int Id { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdEvento { get; set; }
}
