using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActivosFijosDotNetCore.Models
{
    public partial class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public int IdDepartamento { get; set; }
        public string TipoPersona { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Fecha Ingreso")]
        public DateTime FechaIngreso { get; set; }
        public string IdEstado { get; set; }

        public Departamento IdDepartamentoNavigation { get; set; }
        public Estado IdEstadoNavigation { get; set; }
    }
}
