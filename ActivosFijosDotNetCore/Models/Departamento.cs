using System;
using System.Collections.Generic;

namespace ActivosFijosDotNetCore.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            ActivosFijos = new HashSet<ActivosFijos>();
            Empleado = new HashSet<Empleado>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string IdEstado { get; set; }

        public Estado IdEstadoNavigation { get; set; }
        public ICollection<ActivosFijos> ActivosFijos { get; set; }
        public ICollection<Empleado> Empleado { get; set; }
    }
}
