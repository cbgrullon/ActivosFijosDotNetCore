using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ActivosFijosDotNetCore.Models;

namespace ActivosFijosDotNetCore.Controllers
{
    public class ActivosFijosController : Controller
    {
        private readonly ActivosFijosDBContext _context;

        public ActivosFijosController(ActivosFijosDBContext context)
        {
            _context = context;
        }

        // GET: ActivosFijos
        public async Task<IActionResult> Index()
        {
            var activosFijosDBContext = _context.ActivosFijos.Include(a => a.IdDepartamentoNavigation).Include(a => a.IdEstadoNavigation).Include(a => a.IdTipoActivoNavigation);
            return View(await activosFijosDBContext.ToListAsync());
        }

        // GET: ActivosFijos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activosFijos = await _context.ActivosFijos
                .Include(a => a.IdDepartamentoNavigation)
                .Include(a => a.IdEstadoNavigation)
                .Include(a => a.IdTipoActivoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activosFijos == null)
            {
                return NotFound();
            }

            return View(activosFijos);
        }

        // GET: ActivosFijos/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Descripcion");
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion");
            ViewData["IdTipoActivo"] = new SelectList(_context.TipoActivo, "Id", "CuentaCompra");
            return View();
        }

        // POST: ActivosFijos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,IdDepartamento,IdTipoActivo,ValorCompra,DepreciacionPorAnno,AnnosDeDepreciacion,IdEstado")] ActivosFijos activosFijos)
        {
            if (ModelState.IsValid)
            {
                activosFijos.FechaRegistro = DateTime.Now;
                _context.Add(activosFijos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Descripcion", activosFijos.IdDepartamento);
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion", activosFijos.IdEstado);
            ViewData["IdTipoActivo"] = new SelectList(_context.TipoActivo, "Id", "Descripcion", activosFijos.IdTipoActivo);
            return View(activosFijos);
        }

        // GET: ActivosFijos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activosFijos = await _context.ActivosFijos.FindAsync(id);
            if (activosFijos == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Descripcion", activosFijos.IdDepartamento);
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion", activosFijos.IdEstado);
            ViewData["IdTipoActivo"] = new SelectList(_context.TipoActivo, "Id", "Descripcion", activosFijos.IdTipoActivo);
            return View(activosFijos);
        }

        // POST: ActivosFijos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,IdDepartamento,IdTipoActivo,FechaRegistro,ValorCompra,DepreciacionPorAnno,AnnosDeDepreciacion,IdEstado")] ActivosFijos activosFijos)
        {
            if (id != activosFijos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activosFijos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivosFijosExists(activosFijos.Id))
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
            ViewData["IdDepartamento"] = new SelectList(_context.Departamento, "Id", "Descripcion", activosFijos.IdDepartamento);
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion", activosFijos.IdEstado);
            ViewData["IdTipoActivo"] = new SelectList(_context.TipoActivo, "Id", "Descripcion", activosFijos.IdTipoActivo);
            return View(activosFijos);
        }

        // GET: ActivosFijos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activosFijos = await _context.ActivosFijos
                .Include(a => a.IdDepartamentoNavigation)
                .Include(a => a.IdEstadoNavigation)
                .Include(a => a.IdTipoActivoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (activosFijos == null)
            {
                return NotFound();
            }

            return View(activosFijos);
        }

        // POST: ActivosFijos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activosFijos = await _context.ActivosFijos.FindAsync(id);
            _context.ActivosFijos.Remove(activosFijos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivosFijosExists(int id)
        {
            return _context.ActivosFijos.Any(e => e.Id == id);
        }
    }
}
