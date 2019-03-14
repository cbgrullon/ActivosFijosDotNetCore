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
    public class TiposActivosController : Controller
    {
        private readonly ActivosFijosDBContext _context;

        public TiposActivosController(ActivosFijosDBContext context)
        {
            _context = context;
        }

        // GET: TiposActivos
        public async Task<IActionResult> Index()
        {
            var activosFijosDBContext = _context.TipoActivo.Include(t => t.IdEstadoNavigation);
            return View(await activosFijosDBContext.ToListAsync());
        }

        // GET: TiposActivos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoActivo = await _context.TipoActivo
                .Include(t => t.IdEstadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoActivo == null)
            {
                return NotFound();
            }

            return View(tipoActivo);
        }

        // GET: TiposActivos/Create
        public IActionResult Create()
        {
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion");
            return View();
        }

        // POST: TiposActivos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descripcion,CuentaCompra,CuentaDepreciacion,IdEstado")] TipoActivo tipoActivo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoActivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion", tipoActivo.IdEstado);
            return View(tipoActivo);
        }

        // GET: TiposActivos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoActivo = await _context.TipoActivo.FindAsync(id);
            if (tipoActivo == null)
            {
                return NotFound();
            }
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion", tipoActivo.IdEstado);
            return View(tipoActivo);
        }

        // POST: TiposActivos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,CuentaCompra,CuentaDepreciacion,IdEstado")] TipoActivo tipoActivo)
        {
            if (id != tipoActivo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoActivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoActivoExists(tipoActivo.Id))
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
            ViewData["IdEstado"] = new SelectList(_context.Estado, "Id", "Descripcion", tipoActivo.IdEstado);
            return View(tipoActivo);
        }

        // GET: TiposActivos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoActivo = await _context.TipoActivo
                .Include(t => t.IdEstadoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoActivo == null)
            {
                return NotFound();
            }

            return View(tipoActivo);
        }

        // POST: TiposActivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoActivo = await _context.TipoActivo.FindAsync(id);
            _context.TipoActivo.Remove(tipoActivo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoActivoExists(int id)
        {
            return _context.TipoActivo.Any(e => e.Id == id);
        }
    }
}
