using System;
using System.Collections.Generic;

namespace ActivosFijosDotNetCore.Models
{
    public partial class TipoActivo
    {
        public TipoActivo()
        {
            ActivosFijos = new HashSet<ActivosFijos>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string CuentaCompra { get; set; }
        public string CuentaDepreciacion { get; set; }
        public string IdEstado { get; set; }

        public Estado IdEstadoNavigation { get; set; }
        public ICollection<ActivosFijos> ActivosFijos { get; set; }
    }
}
