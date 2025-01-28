using System;
using System.Collections.Generic;

namespace ApiPruebaTecnica.Modelo;

public partial class Usuario
{
    public long Cedula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime FechaAcceso { get; set; }

    public long Puntaje { get; set; }
}
