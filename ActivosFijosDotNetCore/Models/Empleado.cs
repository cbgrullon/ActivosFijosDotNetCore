using System;
using System.Collections.Generic;

namespace ActivosFijosDotNetCore.Models
{
    public partial class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public int IdDepartamento { get; set; }
        public string TipoPersona { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string IdEstado { get; set; }

        public Departamento IdDepartamentoNavigation { get; set; }
        public Estado IdEstadoNavigation { get; set; }
    }
}
