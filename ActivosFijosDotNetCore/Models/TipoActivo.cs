using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActivosFijosDotNetCore.Models
{
    public partial class TipoActivo
    {
        public TipoActivo()
        {
            ActivosFijos = new HashSet<ActivosFijos>();
        }

        public int Id { get; set; }
        [Display(Name="Tipo Activo")]
        public string Descripcion { get; set; }
        [Display(Name ="Cuenta de Compra")]
        public string CuentaCompra { get; set; }
        [Display(Name ="Cuenta Depreciacion")]
        public string CuentaDepreciacion { get; set; }
        [Display(Name ="Estado")]
        public string IdEstado { get; set; }
        [Display(Name = "Estado")]
        public Estado IdEstadoNavigation { get; set; }
        public ICollection<ActivosFijos> ActivosFijos { get; set; }
    }
}
