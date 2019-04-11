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
        public async Task<IActionResult> Index(SolicitudAsientoViewModel solicitud)
        {
            if (ModelState.IsValid)
            {
                using (var caller = new ApiCaller(configuration))
                {
                    var detalles = new List<Detalle>();
                    decimal MontoDB = 0;
                    decimal MontoCR = 0;
                    foreach (var af in ActivosFijosDB.ActivosFijos)
                    {
                        var cd = af.CalculoDepreciacion.ToList();
                        if (!cd.Exists(x => x.FechaProceso > solicitud.FechaInicio && x.FechaProceso < solicitud.FechaFin))
                        {
                            decimal depreciado = af.DepreciacionPorAnno / 12;
                            MontoDB += depreciado;
                            decimal totalDepreciado = 0;
                            if (cd.Count > 0)
                                totalDepreciado = cd.Max(x => x.DepreciacionAcumulada);
                            totalDepreciado += MontoDB;
                            MontoCR = af.ValorCompra - totalDepreciado;
                            TipoActivo ta = ActivosFijosDB.TipoActivo.FirstOrDefault(x=>x.Id==af.IdTipoActivo);
                            ActivosFijosDB.CalculoDepreciacion.Add(new CalculoDepreciacion
                            {
                                IdActivoFijo = af.Id,
                                DepreciacionAcumulada = totalDepreciado,
                                FechaProceso = solicitud.FechaFin,
                                CuentaDepreciacion = ta.CuentaDepreciacion,
                                CuentaCompra = ta.CuentaCompra,
                                MontoDepreciado = depreciado
                            });
                            detalles.Add(new Detalle
                            {
                                Monto = MontoDB,
                                NumeroCuenta = ta.CuentaCompra,
                                TipoTransaccion = "DB"
                            });
                            detalles.Add(new Detalle
                            {
                                Monto = MontoCR,
                                NumeroCuenta = ta.CuentaDepreciacion,
                                TipoTransaccion = "CR"
                            });
                        }
                    }

                    var itsOk = await caller.ConsumoContabilidad(new ApiRequest
                    {
                        Auxiliar = "8",
                        Descripcion = $"Generacion de Asientos de Activos Fijos {solicitud.FechaFin.ToString("ddMMyyyy")}",
                        Moneda = "DOP",
                        Detalle = detalles
                    });
                }
                return View("Resultado", new ResultadoApiViewModel { Message="Completado"});
            }
            return View(solicitud);
        }
    }
}