using System;
using System.Collections.Generic;

namespace BaseDatosTarea1.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            Empleados = new HashSet<Empleado>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Empleado> Empleados { get; set; }
    }
}
