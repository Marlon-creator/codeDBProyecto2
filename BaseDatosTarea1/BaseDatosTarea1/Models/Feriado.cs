using System;
using System.Collections.Generic;

namespace BaseDatosTarea1.Models
{
    public partial class Feriado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime Fecha { get; set; }
    }
}
