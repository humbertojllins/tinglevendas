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
    public class ClickController : Controller
    {
        private readonly TingleVendasContext _context;

        public ClickController(TingleVendasContext context)
        {
            _context = context;
        }

        // GET: Click
        public async Task<IActionResult> Index(string pNomeCliente)
        {
            setaDadosSessao();

            return View(_context.Click.Where(c => c.NomeCliente == pNomeCliente).OrderBy(c => c.Inicioagendamento).ToList());
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
                ViewData["cpf"] = usuario.Cpf;
                ViewData["grupo"] = usuario.IdGrupo;
                ////ViewData["foto"] = usuario.Foto;
                ViewData["url"] = "Histórico do cliente";
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


        // GET: Click/Details/5
        public async Task<IActionResult> Details(int? id, string pNomeCliente)
        {
            ViewData["pNomeCliente"] = pNomeCliente;
            if (id == null)
            {
                return NotFound();
            }

            var click = await _context.Click
                .FirstOrDefaultAsync(m => m.Id == id);
            if (click == null)
            {
                return NotFound();
            }

            return View(click);
        }

        // GET: Click/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Click/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DataCarga,Id,Atividade,GrupoUnidade,Inicioagendamento,Fimagendamento,Filial,Ddd,FilialIi,Gc,Gv,GrupoCanal,CodSap,NomePdv,NumOs,IndVendaConjunta,DataFechamento,DataFimPriAgendamento,DataFimUltAgendamento,QtdReagendamentoClick,Agendado,Codencerramento,Estado,Safra,NomeMunicipio,Gram,Gra,Matriculatecnico,Tecnico,MatriculaVendedor,Nrba,GcCarteira,Contato1,Contato2,Contato3,Contatoefetivo,NomeCliente,CpfCliente,Coordenador,TelCoord,Prontoparaexecucao,TipoLogr,NomeLogr,NumPorta,TipoCompl,Compl,Bairro,Cep,NumLoc")] Click click)
        {
            if (ModelState.IsValid)
            {
                _context.Add(click);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(click);
        }

        // GET: Click/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var click = await _context.Click.FindAsync(id);
            if (click == null)
            {
                return NotFound();
            }
            return View(click);
        }

        // POST: Click/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DataCarga,Id,Atividade,GrupoUnidade,Inicioagendamento,Fimagendamento,Filial,Ddd,FilialIi,Gc,Gv,GrupoCanal,CodSap,NomePdv,NumOs,IndVendaConjunta,DataFechamento,DataFimPriAgendamento,DataFimUltAgendamento,QtdReagendamentoClick,Agendado,Codencerramento,Estado,Safra,NomeMunicipio,Gram,Gra,Matriculatecnico,Tecnico,MatriculaVendedor,Nrba,GcCarteira,Contato1,Contato2,Contato3,Contatoefetivo,NomeCliente,CpfCliente,Coordenador,TelCoord,Prontoparaexecucao,TipoLogr,NomeLogr,NumPorta,TipoCompl,Compl,Bairro,Cep,NumLoc")] Click click)
        {
            if (id != click.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(click);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClickExists(click.Id))
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
            return View(click);
        }

        // GET: Click/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var click = await _context.Click
                .FirstOrDefaultAsync(m => m.Id == id);
            if (click == null)
            {
                return NotFound();
            }

            return View(click);
        }

        // POST: Click/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var click = await _context.Click.FindAsync(id);
            _context.Click.Remove(click);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClickExists(int id)
        {
            return _context.Click.Any(e => e.Id == id);
        }
    }
}
