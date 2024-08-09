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
    public class SupervisorController : Controller
    {
        private readonly TingleVendasContext _context;

        public SupervisorController(TingleVendasContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Seta os dados de sessao do usuario para ser utilizada na pagina _layout
        /// </summary>
        private void setaDadosSessao()
        {
            var usuario = HttpContext.Session.GetObjectFromJson<Usuario>("sessionUsuario");

            if (usuario != null)
            {
                //ViewData["nome"] = usuario.Nome + " " + usuario.Sobrenome;
                ViewData["nome"] = usuario.Nome + " " + usuario.Sobrenome;
                ViewData["email"] = usuario.Email;
                ////ViewData["foto"] = usuario.Foto;
                ViewData["url"] = "Cadastrar Supervisor";
                //this.ControllerContext.RouteData.Values["action"].ToString();

                int totalMenus = _context.Menu.Count();
                //Esconde os menus
                for (int i = 1; i <= totalMenus; i++)
                {
                    ViewData[i.ToString()] = "none";
                }

                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    ViewData["ListaMenu"] = new SelectList(_context.GrupoMenu.Where(gm => gm.IdGrupo == usuario.IdGrupo), "Idmenu", "Idmenu");
                    //Mostra os menus para o perfil do usuário
                    foreach (var item in ((SelectList)ViewData["ListaMenu"]))
                    {
                        ViewData[item.Value] = "Normal";
                    }
                }
            }
        }

        // GET: Supervisor
        public async Task<IActionResult> Index()
        {
            setaDadosSessao();
            var tingleVendasContext = _context.Supervisor.Include(s => s.IdCoordenadorNavigation);

            /*
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nome" : "";
            ViewBag.CpfSortParm = String.IsNullOrEmpty(sortOrder) ? "cpf" : "";
            ViewBag.SupervisorSortParm = String.IsNullOrEmpty(sortOrder) ? "supervisor" : "";
            var filtro = from s in _context.Supervisor.Include(s => s.IdCoordenadorNavigation)
                         select s;
            switch (sortOrder)
            {
                case "nome":
                    filtro = filtro.OrderByDescending(s => s.Nome);
                    break;
                case "cpf":
                    filtro = filtro.OrderByDescending(s => s.Cpf);
                    break;
                case "supervisor":
                    filtro = filtro.OrderByDescending(s => s.IdCoordenadorNavigation.Nome);
                    break;
            }
            return View(filtro.ToList());
            */

            return View(await tingleVendasContext.ToListAsync());
        }

        // GET: Supervisor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisor
                .Include(s => s.IdCoordenadorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        // GET: Supervisor/Create
        public IActionResult Create()
        {
            setaDadosSessao();
            ViewData["IdCoordenador"] = new SelectList(_context.Coordenador, "Id", "Nome");
            return View();
        }

        // POST: Supervisor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cpf,Nome,IdCoordenador,Interno")] Supervisor supervisor)
        {
            setaDadosSessao();
            if (ModelState.IsValid)
            {
                _context.Add(supervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            }

            ViewData["IdCoordenador"] = new SelectList(_context.Coordenador, "Id", "Nome", supervisor.IdCoordenador);
            return View(supervisor);
        }

        // GET: Supervisor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisor.FindAsync(id);
            if (supervisor == null)
            {
                return NotFound();
            }
            ViewData["IdCoordenador"] = new SelectList(_context.Coordenador, "Id", "Nome", supervisor.IdCoordenador);
            return View(supervisor);
        }

        // POST: Supervisor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cpf,Nome,IdCoordenador,Interno")] Supervisor supervisor)
        {
            setaDadosSessao();
            if (id != supervisor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(supervisor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupervisorExists(supervisor.Id))
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
            ViewData["IdCoordenador"] = new SelectList(_context.Coordenador, "Id", "Nome", supervisor.IdCoordenador);
            return View(supervisor);
        }

        // GET: Supervisor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var supervisor = await _context.Supervisor
                .Include(s => s.IdCoordenadorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        // POST: Supervisor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var supervisor = await _context.Supervisor.FindAsync(id);
            _context.Supervisor.Remove(supervisor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupervisorExists(int id)
        {
            return _context.Supervisor.Any(e => e.Id == id);
        }

        [HttpPost]
        public ActionResult SupervisorCpfExiste(string cpf, int? id)
        {
            bool cpfExiste = false;
            try
            {
                if (id.HasValue)
                {
                    cpfExiste = _context.Supervisor.Any(e => e.Cpf == cpf && e.Id != id);
                }
                else
                {
                    cpfExiste = _context.Supervisor.Any(e => e.Cpf == cpf);
                }
                return Json(!cpfExiste);
            }
            catch
            {
                return Json(false);
            }
        }
    }
}
