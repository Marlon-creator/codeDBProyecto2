using System;
using System.Collections.Generic;

namespace BaseDatosTarea1.Models
{
    public partial class Empleado
    {
        public int Id { get; set; }
        public int IdDepartamento { get; set; }
        public int IdTipoDocuIdentidad { get; set; }
        public int ValorDocuIdentidad { get; set; }
        public string Puesto { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public bool? Activo { get; set; }

        public virtual Departamento IdDepartamentoNavigation { get; set; } = null!;
        public virtual TipoDocuIdentidad IdTipoDocuIdentidadNavigation { get; set; } = null!;
    }
}
