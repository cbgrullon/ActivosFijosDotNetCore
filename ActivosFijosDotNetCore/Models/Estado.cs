using System;
using System.Collections.Generic;

namespace ActivosFijosDotNetCore.Models
{
    public partial class Estado
    {
        public Estado()
        {
            ActivosFijos = new HashSet<ActivosFijos>();
            Departamento = new HashSet<Departamento>();
            Empleado = new HashSet<Empleado>();
            TipoActivo = new HashSet<TipoActivo>();
        }

        public string Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<ActivosFijos> ActivosFijos { get; set; }
        public ICollection<Departamento> Departamento { get; set; }
        public ICollection<Empleado> Empleado { get; set; }
        public ICollection<TipoActivo> TipoActivo { get; set; }
    }
}
