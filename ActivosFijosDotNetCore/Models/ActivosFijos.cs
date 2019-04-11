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
        [Required(ErrorMessage ="Campo descripcion es Requerido")]
        public string Descripcion { get; set; }
        [Display(Name="Departamento")]
        [Required(ErrorMessage ="Debe seleccionar un Departamento")]
        public int IdDepartamento { get; set; }
        [Display(Name= "Tipo Activo")]
        public int IdTipoActivo { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name="Fecha Registro")]
        public DateTime FechaRegistro { get; set; }
        [Display(Name="Valor de Compra")]
        [Range(1,int.MaxValue)]
        [Required(ErrorMessage ="Campo Valor de Compra es Requerido")]
        public decimal ValorCompra { get; set; }
        [Display(Name ="Depreciacion Por Año")]
        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "Campo Depreciacion por Año es Requerido")]
        public decimal DepreciacionPorAnno { get; set; }
        [Display(Name = "Años para Depreciarse completamente")]
        [Range(1, int.MaxValue)]
        [Required(ErrorMessage = "Campo Años para Depreciarse completamente es Requerido")]
        public int AnnosDeDepreciacion { get; set; }
        [Display(Name ="Estado")]
        [Required(ErrorMessage = "Debe seleccionar un Estado")]
        public string IdEstado { get; set; }
        [Display(Name = "Departamento")]
        public Departamento IdDepartamentoNavigation { get; set; }
        [Display(Name = "Estado")]
        public Estado IdEstadoNavigation { get; set; }
        [Display(Name = "Tipo Activo")]
        public TipoActivo IdTipoActivoNavigation { get; set; }
        public ICollection<CalculoDepreciacion> CalculoDepreciacion { get; set; }
    }
}
