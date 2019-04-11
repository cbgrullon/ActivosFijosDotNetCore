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
                            var totalDepreciado = cd.Max(x => x.DepreciacionAcumulada);
                            totalDepreciado += MontoDB;
                            MontoCR = af.ValorCompra - totalDepreciado;
                            ActivosFijosDB.CalculoDepreciacion.Add(new CalculoDepreciacion
                            {
                                IdActivoFijo = af.Id,
                                DepreciacionAcumulada = totalDepreciado,
                                FechaProceso = solicitud.FechaFin,
                                CuentaDepreciacion = af.IdTipoActivoNavigation.CuentaDepreciacion,
                                CuentaCompra = af.IdTipoActivoNavigation.CuentaCompra,
                                MontoDepreciado = depreciado
                            });
                            detalles.Add(new Detalle
                            {
                                Monto=MontoDB,
                                NumeroCuenta= af.IdTipoActivoNavigation.CuentaCompra,
                                TipoTransaccion="DB"
                            });
                            detalles.Add(new Detalle
                            {
                                Monto=MontoCR,
                                NumeroCuenta = af.IdTipoActivoNavigation.CuentaDepreciacion,
                                TipoTransaccion= "CR"
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
                return RedirectToAction("Index","Home");
            }
            return View(solicitud);
        }
    }
}