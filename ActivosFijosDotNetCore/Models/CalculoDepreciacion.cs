using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ActivosFijosDotNetCore.Models
{
    public partial class CalculoDepreciacion
    {
        public int Id { get; set; }
        [Display(Name="Fecha Proceso")]
        public DateTime FechaProceso { get; set; }
        public int IdActivoFijo { get; set; }
        public decimal MontoDepreciado { get; set; }
        public decimal DepreciacionAcumulada { get; set; }
        public string CuentaCompra { get; set; }
        public string CuentaDepreciacion { get; set; }

        public ActivosFijos IdActivoFijoNavigation { get; set; }
    }
}
