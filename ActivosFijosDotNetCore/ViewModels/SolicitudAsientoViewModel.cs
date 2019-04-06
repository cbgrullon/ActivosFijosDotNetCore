using System;
using System.ComponentModel.DataAnnotations;

namespace ActivosFijosDotNetCore.ViewModels
{
    public class SolicitudAsientoViewModel
    {
        [Required]
        [Display(Name = "Fecha Inicio")]
        public DateTime FechaInicio { get; set; }
        [Required]
        [Display(Name = "Fecha Fin")]
        public DateTime FechaFin { get; set; }
    }
}
