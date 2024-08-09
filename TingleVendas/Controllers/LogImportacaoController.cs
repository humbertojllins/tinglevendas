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
    public class LogImportacaoController : Controller
    {
        private readonly TingleVendasContext _context;

        public LogImportacaoController(TingleVendasContext context)
        {
            _context = context;
        }

        // GET: LogImportacao
        public async Task<IActionResult> Index(string pPlanilha, string pChave1)
        {
            setaDadosSessao();
            var listaPlanilhas = (from l in _context.LogImportacao
                         select new
                         {
                             Planilha = l.Planilha
                         }).Distinct();
            ViewData["Planilha"] = new SelectList(listaPlanilhas, "Planilha", "Planilha", pPlanilha);
            var lista = (from l in _context.LogImportacao
                         select new
                         {
                             Chave1 = l.Chave1
                         }).Distinct();
            ViewData["Chave1"] = new SelectList(lista, "Chave1", "Chave1",pChave1);

            if (pChave1 != null && pChave1!="")
            {
                return View(await _context.LogImportacao.Where(li => li.Planilha == Convert.ToString(pPlanilha) && li.Chave1==pChave1).ToListAsync());
                /*var listaLog = from ll in _context.LogImportacao
                               where ll.Planilha == pPlanilha
                               where ll.Chave1 == pChave1
                               select new LogImportacao2()
                               {

                                   DataHora = Convert.ToDateTime(ll.DataHora),
                                   Chave1 = ll.Chave1,
                                   Chave2 = ll.Chave2,
                                   Chave3 = ll.Chave3,
                                   Linha = ll.Linha,
                                   Erro = ll.Erro

                               };
                return View(listaLog);*/
            }
            else
            {
                
                /*var listaLog = from ll in _context.LogImportacao
                               where ll.Planilha == pPlanilha
                               select new LogImportacao2()
                               {
                                   Id = ll.Id,
                                   DataHora = Convert.ToDateTime(ll.DataHora),
                                   Chave1 = ll.Chave1,
                                   Chave2 = ll.Chave2,
                                   Chave3 = ll.Chave3,
                                   Linha = ll.Linha,
                                   Erro = ll.Erro

                               };
                return View(listaLog);*/
                return View(await _context.LogImportacao.Where(li => li.Planilha == Convert.ToString(pPlanilha)).ToListAsync());
            }
            
        }


        // GET: LogImportacao/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logImportacao = await _context.LogImportacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logImportacao == null)
            {
                return NotFound();
            }

            return View(logImportacao);
        }

        // GET: LogImportacao/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LogImportacao/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Planilha,DataHora,Chave1,Chave2,Chave3,Linha,Erro")] LogImportacao logImportacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logImportacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(logImportacao);
        }

        // GET: LogImportacao/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logImportacao = await _context.LogImportacao.FindAsync(id);
            if (logImportacao == null)
            {
                return NotFound();
            }
            return View(logImportacao);
        }

        // POST: LogImportacao/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Planilha,DataHora,Chave1,Chave2,Chave3,Linha,Erro")] LogImportacao logImportacao)
        {
            if (id != logImportacao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logImportacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogImportacaoExists(logImportacao.Id))
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
            return View(logImportacao);
        }

        // GET: LogImportacao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logImportacao = await _context.LogImportacao
                .FirstOrDefaultAsync(m => m.Id == id);
            if (logImportacao == null)
            {
                return NotFound();
            }

            return View(logImportacao);
        }

        // POST: LogImportacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logImportacao = await _context.LogImportacao.FindAsync(id);
            _context.LogImportacao.Remove(logImportacao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogImportacaoExists(int id)
        {
            return _context.LogImportacao.Any(e => e.Id == id);
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
                ViewData["url"] = "Log de importações";
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
    }
}
