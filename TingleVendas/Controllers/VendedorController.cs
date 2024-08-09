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
    public class VendedorController : Controller
    {
        private readonly TingleVendasContext _context;

        public VendedorController(TingleVendasContext context)
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
                ViewData["url"] = "Cadastrar Vendedor";
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

        // GET: Vendedor
        public async Task<IActionResult> Index()
        {
            setaDadosSessao();
            var tingleVendasContext = _context.Vendedor.Include(v => v.IdCanalVendasNavigation).Include(v => v.IdSupervisorNavigation).ToListAsync();
            return View(await tingleVendasContext);
        }

        // GET: Vendedor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            setaDadosSessao();
            //ViewData["IdCanalVendas"] = new SelectList(_context.CanalVendas, "Id", "Descricao");
            //ViewData["IdSupervisor"] = new SelectList(_context.Supervisor, "Id", "Nome");
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedor
                .Include(v => v.IdCanalVendasNavigation)
                .Include(v => v.IdSupervisorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // GET: Vendedor/Create
        public IActionResult Create()
        {
            setaDadosSessao();
            ViewData["IdCanalVendas"] = new SelectList(_context.CanalVendas, "Id", "Descricao");
            ViewData["IdSupervisor"] = new SelectList(_context.Supervisor, "Id", "Nome");
            return View();
        }

        // POST: Vendedor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cpf,Nome,IdSupervisor,IdCanalVendas,Interno")] Vendedor vendedor)
        {
            setaDadosSessao();
            if (ModelState.IsValid)
            {
                _context.Add(vendedor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCanalVendas"] = new SelectList(_context.CanalVendas, "Id", "Descricao", vendedor.IdCanalVendas);
            ViewData["IdSupervisor"] = new SelectList(_context.Supervisor, "Id", "Nome", vendedor.IdSupervisor);
            return View(vendedor);
        }

        // GET: Vendedor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedor.FindAsync(id);
            if (vendedor == null)
            {
                return NotFound();
            }
            ViewData["IdCanalVendas"] = new SelectList(_context.CanalVendas, "Id", "Descricao", vendedor.IdCanalVendas);
            ViewData["IdSupervisor"] = new SelectList(_context.Supervisor, "Id", "Nome", vendedor.IdSupervisor);
            return View(vendedor);
        }

        // POST: Vendedor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cpf,Nome,IdSupervisor,IdCanalVendas,Interno")] Vendedor vendedor)
        {
            setaDadosSessao();
            if (id != vendedor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendedor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendedorExists(vendedor.Id))
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
            ViewData["IdCanalVendas"] = new SelectList(_context.CanalVendas, "Id", "Id", vendedor.IdCanalVendas);
            ViewData["IdSupervisor"] = new SelectList(_context.Supervisor, "Id", "Id", vendedor.IdSupervisor);
            return View(vendedor);
        }

        // GET: Vendedor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var vendedor = await _context.Vendedor
                .Include(v => v.IdCanalVendasNavigation)
                .Include(v => v.IdSupervisorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendedor == null)
            {
                return NotFound();
            }

            return View(vendedor);
        }

        // POST: Vendedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            setaDadosSessao();
            var vendedor = await _context.Vendedor.FindAsync(id);
            _context.Vendedor.Remove(vendedor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendedorExists(int id)
        {
            return _context.Vendedor.Any(e => e.Id == id);
        }

        [HttpPost]
        public ActionResult VendedorCpfExiste(string cpf, int? id)
        {
            bool cpfExiste = false;
            try
            {
                if (id.HasValue)
                {
                    cpfExiste = _context.Vendedor.Any(e => e.Cpf == cpf && e.Id != id);
                }
                else
                {
                    cpfExiste = _context.Vendedor.Any(e => e.Cpf == cpf);
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
