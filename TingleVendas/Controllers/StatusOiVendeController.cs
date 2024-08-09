using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TingleVendas.Models;

namespace TingleVendas.Controllers
{
    [Authorize]
    public class StatusOiVendeController : Controller
    {
        private readonly TingleVendasContext _context;

        public StatusOiVendeController(TingleVendasContext context)
        {
            _context = context;
        }

        // GET: StatusOiVende
        public async Task<IActionResult> Index()
        {
            return View(await _context.StatusOiVende.ToListAsync());
        }

        // GET: StatusOiVende/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusOiVende = await _context.StatusOiVende
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusOiVende == null)
            {
                return NotFound();
            }

            return View(statusOiVende);
        }

        // GET: StatusOiVende/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusOiVende/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DescStatus")] StatusOiVende statusOiVende)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusOiVende);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusOiVende);
        }

        // GET: StatusOiVende/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusOiVende = await _context.StatusOiVende.FindAsync(id);
            if (statusOiVende == null)
            {
                return NotFound();
            }
            return View(statusOiVende);
        }

        // POST: StatusOiVende/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DescStatus")] StatusOiVende statusOiVende)
        {
            if (id != statusOiVende.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusOiVende);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusOiVendeExists(statusOiVende.Id))
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
            return View(statusOiVende);
        }

        // GET: StatusOiVende/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusOiVende = await _context.StatusOiVende
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusOiVende == null)
            {
                return NotFound();
            }

            return View(statusOiVende);
        }

        // POST: StatusOiVende/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusOiVende = await _context.StatusOiVende.FindAsync(id);
            _context.StatusOiVende.Remove(statusOiVende);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusOiVendeExists(int id)
        {
            return _context.StatusOiVende.Any(e => e.Id == id);
        }
    }
}
