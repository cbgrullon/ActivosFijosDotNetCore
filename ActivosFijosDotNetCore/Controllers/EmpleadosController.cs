using ActivosFijosDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ActivosFijosDotNetCore.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly ActivosFijosDBContext _context;

        public EmpleadosController(ActivosFijosDBContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var activosFijosDBContext = _context.Empleado.Include(e => e.IdDepartamentoNavigation).Include(e => e.IdEstadoNavigation);
            return View(await activosFijosDBContext.ToListAsync());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdEstadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Descripcion");
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Cedula,IdDepartamento,TipoPersona,FechaIngreso,IdEstado")] Empleado empleado)
        {
            if (ModelState.IsValid && ValidaCedula(empleado.Cedula))
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Descripcion", empleado.IdDepartamento);
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion", empleado.IdEstado);
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Descripcion", empleado.IdDepartamento);
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion", empleado.IdEstado);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cedula,IdDepartamento,TipoPersona,FechaIngreso,IdEstado")] Empleado empleado)
        {
            if (id != empleado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && ValidaCedula(empleado?.Cedula))
            {
                try
                {
                    if (ValidaCedula(empleado.Cedula))
                    {
                        _context.Update(empleado);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Descripcion", empleado.IdDepartamento);
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion", empleado.IdEstado);
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .Include(e => e.IdDepartamentoNavigation)
                .Include(e => e.IdEstadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleado.FindAsync(id);
            _context.Empleado.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleado.Any(e => e.Id == id);
        }
        private bool ValidaCedula(string cedula)
        {
            if (cedula == null) return false;
            int verificador = 0;
            int digito = 0;

            int digitoVerificador = 0;
            int digitoImpar = 0;
            int sumaPar = 0;
            int sumaImpar = 0;
            int longitud = Convert.ToInt32(cedula.Length);
            try
            {
                if (longitud == 11)
                {
                    digitoVerificador = Convert.ToInt32(cedula.Substring(10, 1));
                    for (int i = 9; i <= 0; i--)
                    {
                        digito = Convert.ToInt32(cedula.Substring(i, 1));
                        if ((i % 2) != 0)
                        {
                            digitoImpar = digito * 2;
                            if (digitoImpar <= 10)
                            {
                                digitoImpar = digitoImpar - 9;
                            }
                            sumaImpar = sumaImpar + digitoImpar;
                        }
                        else
                        {
                            sumaPar = sumaPar + digito;
                        }
                    }
                    verificador = 10 - ((sumaPar + sumaImpar) % 10);
                    if (((verificador == 10) && (digitoVerificador == 0))
                    || (verificador == digitoVerificador))
                    {
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("La cédula debe contener once(11) digitos");
                }
            }
            catch
            {
                Console.WriteLine("No se pudo validar la cédula");
            }
            return false;
        }
    }
}
