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
    public class Oi360DetalhesController : Controller
    {
        private readonly TingleVendasContext _context;

        public Oi360DetalhesController(TingleVendasContext context)
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
                ViewData["url"] = "Informações adicionais da venda - Input";
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

        // GET: Oi360Detalhes
        public async Task<IActionResult> Index()
        {
            var tingleVendasContext = _context.Oi360Detalhes.Include(o => o.NumeroPedidoNavigation).Include(o => o.StatusOiVendeNavigation).Include(o => o.SupervisorNavigation).Include(o => o.VendedorNavigation);
            return View(await tingleVendasContext.ToListAsync());
        }

        // GET: Oi360Detalhes/Details/5
        public async Task<IActionResult> Details(string id, int ano, int mes)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var oi360Detalhes = await _context.Oi360Detalhes
                .Include(o => o.NumeroPedidoNavigation)
                .Include(o => o.StatusOiVendeNavigation)
                .Include(o => o.SupervisorNavigation)
                .Include(o => o.VendedorNavigation)
                .FirstOrDefaultAsync(m => m.NumeroPedido == id);
            if (oi360Detalhes == null)
            {
                return RedirectToAction("Create", "Oi360Detalhes", new { numpedido = id, pano=ano, pmes=mes});
            }

            ViewData["anoCorrente"] = ano;
            ViewData["mesCorrente"] = mes;
            return View(oi360Detalhes);
        }

        // GET: Oi360Detalhes/Create
        public IActionResult Create(string numpedido, int pano, int pmes)
        {
            setaDadosSessao();
            ViewData["NumeroPedido"] = new SelectList(_context.Oi360, "NumeroDoPedido", "NumeroDoPedido",numpedido);
            ViewData["StatusOiVende"] = new SelectList(_context.StatusOiVende, "Id", "DescStatus");
            ViewData["Supervisor"] = new SelectList(_context.Supervisor, "Id", "Nome");
            ViewData["Vendedor"] = new SelectList(_context.Vendedor, "Id", "Nome");

            ViewData["anoCorrente"] = pano;
            ViewData["mesCorrente"] = pmes;
            return View();
        }

        // POST: Oi360Detalhes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroPedido,Supervisor,Vendedor,ObservacaoAuditor,StatusOiVende,ProtocoloOiVende,DocumentoVenda,CheckListDocumentoClienteOk,CheckListPropostaOk,CheckListConfirmacaoClienteOk,NotaAtendimento,DataAgendamento,HoraAgendamento,ObservacaoPendencia")] Oi360Detalhes oi360Detalhes, int pano, int pmes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oi360Detalhes);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Oi360",new { pAno=pano, pMes=pmes});
            }
            ViewData["NumeroPedido"] = new SelectList(_context.Oi360, "NumeroDoPedido", "NumeroDoPedido", oi360Detalhes.NumeroPedido);
            ViewData["StatusOiVende"] = new SelectList(_context.StatusOiVende, "Id", "DescStatus", oi360Detalhes.StatusOiVende);
            ViewData["Supervisor"] = new SelectList(_context.Supervisor, "Id", "Nome", oi360Detalhes.Supervisor);
            ViewData["Vendedor"] = new SelectList(_context.Vendedor, "Id", "Nome", oi360Detalhes.Vendedor);
            return View(oi360Detalhes);
        }

        // GET: Oi360Detalhes/Edit/5
        public async Task<IActionResult> Edit(int? id, int pAno, int pMes)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var oi360Detalhes = await _context.Oi360Detalhes.FindAsync(id);
            if (oi360Detalhes == null)
            {
                return NotFound();
            }
            ViewData["NumeroPedido"] = new SelectList(_context.Oi360, "NumeroDoPedido", "NumeroDoPedido", oi360Detalhes.NumeroPedido);
            ViewData["StatusOiVende"] = new SelectList(_context.StatusOiVende, "Id", "DescStatus", oi360Detalhes.StatusOiVende);
            ViewData["Supervisor"] = new SelectList(_context.Supervisor, "Id", "Nome", oi360Detalhes.Supervisor);
            ViewData["Vendedor"] = new SelectList(_context.Vendedor, "Id", "Nome", oi360Detalhes.Vendedor);
            ViewData["anoCorrente"] = pAno;
            ViewData["mesCorrente"] = pMes;

            return View(oi360Detalhes);
        }

        // POST: Oi360Detalhes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroPedido,Supervisor,Vendedor,ObservacaoAuditor,StatusOiVende,ProtocoloOiVende,DocumentoVenda,CheckListDocumentoClienteOk,CheckListPropostaOk,CheckListConfirmacaoClienteOk,NotaAtendimento,DataAgendamento,HoraAgendamento,ObservacaoPendencia")] Oi360Detalhes oi360Detalhes, int pano, int pmes)
        {
            if (id != oi360Detalhes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oi360Detalhes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Oi360DetalhesExists(oi360Detalhes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Details));
                return RedirectToAction("Index", "Oi360", new { pAno=pano, pMes=pmes });
            }
            ViewData["NumeroPedido"] = new SelectList(_context.Oi360, "NumeroDoPedido", "NumeroDoPedido", oi360Detalhes.NumeroPedido);
            ViewData["StatusOiVende"] = new SelectList(_context.StatusOiVende, "Id", "DescStatus", oi360Detalhes.StatusOiVende);
            ViewData["Supervisor"] = new SelectList(_context.Supervisor, "Id", "Nome", oi360Detalhes.Supervisor);
            ViewData["Vendedor"] = new SelectList(_context.Vendedor, "Id", "Nome", oi360Detalhes.Vendedor);
            return View(oi360Detalhes);
        }

        // GET: Oi360Detalhes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oi360Detalhes = await _context.Oi360Detalhes
                .Include(o => o.NumeroPedidoNavigation)
                .Include(o => o.StatusOiVendeNavigation)
                .Include(o => o.SupervisorNavigation)
                .Include(o => o.VendedorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oi360Detalhes == null)
            {
                return NotFound();
            }

            return View(oi360Detalhes);
        }

        // POST: Oi360Detalhes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oi360Detalhes = await _context.Oi360Detalhes.FindAsync(id);
            _context.Oi360Detalhes.Remove(oi360Detalhes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Oi360DetalhesExists(int id)
        {
            return _context.Oi360Detalhes.Any(e => e.Id == id);
        }
    }
}
