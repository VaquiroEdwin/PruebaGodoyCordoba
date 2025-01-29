using System;
using System.Collections.Generic;

namespace ApiPruebaTecnica.Models;

public partial class Usuario
{
    public long Cedula { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime FechaAcceso { get; set; }

    public long Puntaje { get; set; }


    public long CalcularPuntaje()
    {
        var CaracteresNombre = $"{Nombre}{Apellido}".Length;
        int puntaje = 0;

        if (CaracteresNombre > 10)
        {
            puntaje += 20;
        }
        else if (CaracteresNombre > 5)
        {
            puntaje += 10;
        }

        if (Email.Contains("gmail.com", StringComparison.OrdinalIgnoreCase))
        {
            puntaje += 40;
        }
        else if (Email.Contains("hotmail.com", StringComparison.OrdinalIgnoreCase))
        {
            puntaje += 30;
        }
        else
        {
            puntaje += 10;
        }


        return puntaje;
    }

}
