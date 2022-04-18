using System;
using System.Collections.Generic;


namespace BaseDatosTarea1.Models
{
    public partial class Puesto
    {
        
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal SalarioXhora { get; set; }
        public bool? Activo { get; set; }
    }
}
