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
    public class CanalVendasController : Controller
    {
        private readonly TingleVendasContext _context;

        public CanalVendasController(TingleVendasContext context)
        {
            //Versao com alteracao do texto
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
                //////ViewData["foto"] = usuario.Foto;
                ViewData["url"] = "Cadastrar Canal de Vendas";
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

        // GET: CanalVendas
        public async Task<IActionResult> Index()
        {
            setaDadosSessao();
            return View(await _context.CanalVendas.ToListAsync());
        }

        // GET: CanalVendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var canalVendas = await _context.CanalVendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (canalVendas == null)
            {
                return NotFound();
            }

            return View(canalVendas);
        }

        // GET: CanalVendas/Create
        public IActionResult Create()
        {
            setaDadosSessao();
            return View();
        }

        // POST: CanalVendas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] CanalVendas canalVendas)
        {
            setaDadosSessao();
            if (ModelState.IsValid)
            {
                _context.Add(canalVendas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(canalVendas);
        }

        // GET: CanalVendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var canalVendas = await _context.CanalVendas.FindAsync(id);
            if (canalVendas == null)
            {
                return NotFound();
            }
            return View(canalVendas);
        }

        // POST: CanalVendas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] CanalVendas canalVendas)
        {
            setaDadosSessao();
            if (id != canalVendas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(canalVendas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CanalVendasExists(canalVendas.Id))
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
            return View(canalVendas);
        }

        // GET: CanalVendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var canalVendas = await _context.CanalVendas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (canalVendas == null)
            {
                return NotFound();
            }

            return View(canalVendas);
        }

        // POST: CanalVendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            setaDadosSessao();
            var canalVendas = await _context.CanalVendas.FindAsync(id);
            _context.CanalVendas.Remove(canalVendas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CanalVendasExists(int id)
        {
            return _context.CanalVendas.Any(e => e.Id == id);
        }
    }
}
