using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActivosFijosDotNetCore.ViewModels;
using ActivosFijosDotNetCore.Services;
using Microsoft.Extensions.Configuration;
using ActivosFijosDotNetCore.Models;
namespace ActivosFijosDotNetCore.Controllers
{
    public class AsientosController : Controller
    {
        private ActivosFijosDBContext ActivosFijosDB;
        private IConfiguration configuration;
        public AsientosController(IConfiguration configuration, ActivosFijosDBContext ActivosFijosDB)
        {
            this.configuration = configuration;
            this.ActivosFijosDB = ActivosFijosDB;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async IActionResult Index(SolicitudAsientoViewModel solicitud)
        {
            if (ModelState.IsValid)
            {

                using (var caller = new ApiCaller(configuration))
                {
                    var itsOk = await caller.ConsumoContabilidad(new ApiRequest {
                        Auxiliar = "8",
                        Descripcion = "Generacion de Asientos de Activos Fijos",
                        Moneda = "DOP",
                        Detalle = new List<Detalle>
                        {
                            new Detalle
                            {

                            }
                        }
                    });
                }
            }
        }
        private Tuple<decimal,decimal> GeneraCRYDB(DateTime FechaInicio,DateTime FechaFin, IEnumerable<ActivosFijos> source)
        {
            decimal MontoDB = 0;
            decimal MontoCR = 0;
            foreach (var af in source)
            {
                var cd = af.CalculoDepreciacion.ToList();
                if (!cd.Exists(x => x.FechaProceso > FechaInicio && x.FechaProceso < FechaFin))
                {
                    MontoDB= 
                    
                }
            }
            return new Tuple<decimal, decimal>(0, 0);
        }
    }
}