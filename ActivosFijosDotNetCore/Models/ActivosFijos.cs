using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActivosFijosDotNetCore.Models
{
    public partial class ActivosFijos
    {
        public ActivosFijos()
        {
            CalculoDepreciacion = new HashSet<CalculoDepreciacion>();
        }

        public int Id { get; set; }
        [Display(Name="Activo Fijo")]
        public string Descripcion { get; set; }
        [Display(Name ="Departamento")]
        public int IdDepartamento { get; set; }
        [Display(Name ="Tipo Activo")]
        public int IdTipoActivo { get; set; }
        [Display(Name = "Fecha Adicion")]
        public DateTime FechaRegistro { get; set; }
        [Display(Name ="Valor de Compra")]
        public decimal ValorCompra { get; set; }
        [Display(Name ="Depreciacion Acumulada")]
        public decimal DepreciacionAcumulada { get; set; }
        [Display(Name ="Estado")]
        public string IdEstado { get; set; }
        [Display(Name = "Departamento")]
        public Departamento IdDepartamentoNavigation { get; set; }
        [Display(Name = "Estado")]
        public Estado IdEstadoNavigation { get; set; }
        [Display(Name ="Tipo Activo")]
        public TipoActivo IdTipoActivoNavigation { get; set; }
        public ICollection<CalculoDepreciacion> CalculoDepreciacion { get; set; }
    }
}
