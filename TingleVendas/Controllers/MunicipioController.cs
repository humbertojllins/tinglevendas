using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TingleVendas.Models;

namespace TingleVendas.Controllers
{
    public class MunicipioController : Controller
    {
        private readonly TingleVendasContext _context;

        public MunicipioController(TingleVendasContext context)
        {
            _context = context;
        }

        // GET: Municipio
        public async Task<IActionResult> Index()
        {
            var tingleVendasContext = _context.Municipio.Include(m => m.UfNavigation);
            return View(await tingleVendasContext.ToListAsync());
        }

        // GET: Municipio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipio = await _context.Municipio
                .Include(m => m.UfNavigation)
                .FirstOrDefaultAsync(m => m.IdMunicipio == id);
            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);
        }

        // GET: Municipio/Create
        public IActionResult Create()
        {
            ViewData["Uf"] = new SelectList(_context.Uf, "Sigla", "Sigla");
            return View();
        }

        // POST: Municipio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMunicipio,Municipio1,Uf,Ativo")] Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(municipio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Uf"] = new SelectList(_context.Uf, "Sigla", "Sigla", municipio.Uf);
            return View(municipio);
        }

        // GET: Municipio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipio = await _context.Municipio.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }
            ViewData["Uf"] = new SelectList(_context.Uf, "Sigla", "Sigla", municipio.Uf);
            return View(municipio);
        }

        // POST: Municipio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMunicipio,Municipio1,Uf,Ativo")] Municipio municipio)
        {
            if (id != municipio.IdMunicipio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(municipio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MunicipioExists(municipio.IdMunicipio))
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
            ViewData["Uf"] = new SelectList(_context.Uf, "Sigla", "Sigla", municipio.Uf);
            return View(municipio);
        }

        // GET: Municipio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var municipio = await _context.Municipio
                .Include(m => m.UfNavigation)
                .FirstOrDefaultAsync(m => m.IdMunicipio == id);
            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);
        }

        // POST: Municipio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var municipio = await _context.Municipio.FindAsync(id);
            _context.Municipio.Remove(municipio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MunicipioExists(int id)
        {
            return _context.Municipio.Any(e => e.IdMunicipio == id);
        }
    }
}
