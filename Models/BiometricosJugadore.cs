using System;
using System.Collections.Generic;

namespace SportSync.Models;

public partial class BiometricosJugadore
{
    public int IdBiometrico { get; set; }

    public int IdJugador { get; set; }

    public DateTime Fecha { get; set; }

    public int RitmoCardiaco { get; set; }

    public int Pasos { get; set; }

    public decimal Calorias { get; set; }

    public decimal Distancia { get; set; }

    public decimal SuenoHoras { get; set; }

    public virtual Jugadore IdJugadorNavigation { get; set; } = null!;
}
