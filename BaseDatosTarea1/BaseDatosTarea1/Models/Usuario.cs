using System;
using System.Collections.Generic;

namespace BaseDatosTarea1.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
