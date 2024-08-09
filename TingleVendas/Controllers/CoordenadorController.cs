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
    public class CoordenadorController : Controller
    {
        private readonly TingleVendasContext _context;

        public CoordenadorController(TingleVendasContext context)
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
                ViewData["url"] = "Cadastrar Coordenador";
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

        // GET: Coordenador
        public async Task<IActionResult> Index()
        {
            setaDadosSessao();
            return View(await _context.Coordenador.ToListAsync());
        }

        // GET: Coordenador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var coordenador = await _context.Coordenador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordenador == null)
            {
                return NotFound();
            }

            return View(coordenador);
        }

        // GET: Coordenador/Create
        public IActionResult Create()
        {
            setaDadosSessao();
            return View();
        }

        // POST: Coordenador/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cpf,Nome")] Coordenador coordenador)
        {
            setaDadosSessao();
            if (ModelState.IsValid)
            {
                //if (!CoordenadorCadastrado(coordenador.Cpf))
                //{ 
                _context.Add(coordenador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //}
                //else
                //{
                //  return Ok(new { message = "Cpf já cadastrado para outro coordenador!" });
                //}
            }
            return View(coordenador);
        }

        // GET: Coordenador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var coordenador = await _context.Coordenador.FindAsync(id);
            if (coordenador == null)
            {
                return NotFound();
            }
            return View(coordenador);
        }

        // POST: Coordenador/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cpf,Nome")] Coordenador coordenador)
        {
            setaDadosSessao();
            if (id != coordenador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordenador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordenadorExists(coordenador.Id))
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
            return View(coordenador);
        }

        // GET: Coordenador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var coordenador = await _context.Coordenador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordenador == null)
            {
                return NotFound();
            }

            return View(coordenador);
        }

        // POST: Coordenador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coordenador = await _context.Coordenador.FindAsync(id);
            _context.Coordenador.Remove(coordenador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordenadorExists(int id)
        {
            return _context.Coordenador.Any(e => e.Id == id);
        }

        [HttpPost]
        public ActionResult CoordenadorCpfExiste(string cpf, int? id)
        {
            bool cpfExiste = false;
            try
            {
                if (id.HasValue)
                {
                    cpfExiste = _context.Coordenador.Any(e => e.Cpf == cpf && e.Id != id);
                }
                else
                {
                    cpfExiste = _context.Coordenador.Any(e => e.Cpf == cpf);
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
