using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActivosFijosDotNetCore.Models
{
    public partial class Empleado
    {
        public int Id { get; set; }
        [Display(Name="Nombre Completo")]
        public string Nombre { get; set; }
        [Display(Name = "Cedula")]
        public string Cedula { get; set; }
        [Display(Name ="Departamento")]
        public int IdDepartamento { get; set; }
        [Display(Name ="Tipo de Persona")]
        public string TipoPersona { get; set; }
        [Display(Name ="Fecha de Ingreso")]
        public DateTime FechaIngreso { get; set; }
        [Display(Name ="Estadp")]
        public string IdEstado { get; set; }
        [Display(Name ="Departamento")]
        public Departamento IdDepartamentoNavigation { get; set; }
        [Display(Name ="Estado")]
        public Estado IdEstadoNavigation { get; set; }
    }
}
