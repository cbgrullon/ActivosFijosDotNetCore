using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name="Departamento")]
        public string Descripcion { get; set; }
        [Display(Name ="Estado")]
        public string IdEstado { get; set; }
        [Display(Name ="Estado")]
        public Estado IdEstadoNavigation { get; set; }
        public ICollection<ActivosFijos> ActivosFijos { get; set; }
        public ICollection<Empleado> Empleado { get; set; }
    }
}
