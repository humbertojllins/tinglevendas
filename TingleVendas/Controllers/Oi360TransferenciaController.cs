using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TingleVendas.Models;

namespace TingleVendas.Controllers
{
    public class Oi360TransferenciaController : Controller
    {
        private readonly TingleVendasContext _context;
        private IConfiguration _configuration;

        public Oi360TransferenciaController(TingleVendasContext context,IConfiguration config)
        {
            _context = context;
            _configuration = config;
        }

        private void setaDadosSessao()
        {
            var usuario = HttpContext.Session.GetObjectFromJson<Usuario>("sessionUsuario");

            if (usuario != null)
            {
                //ViewData["nome"] = usuario.Nome + " " + usuario.Sobrenome;
                ViewData["nome"] = usuario.Nome + " " + usuario.Sobrenome;
                ViewData["email"] = usuario.Email;
                ViewData["grupo"] = usuario.IdGrupo;
                ////ViewData["foto"] = usuario.Foto;
                ViewData["url"] = "Transferir vendas para perdas";
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

        // GET: Oi360Transferencia
        public async Task<IActionResult> Index(int? pAno, int? pMes, string? pSupervisor)
        {
            setaDadosSessao();
            ViewData["ListaMeses"] = new SelectList(Meses, "Value", "Text", pMes);
            List<Supervisor> listaSup;
            if (ViewData["cpf"] != null && Convert.ToString(ViewData["grupo"]) == "6")
            {
                listaSup = _context.Supervisor.Where(s => s.Cpf == Convert.ToString(ViewData["cpf"])).ToList();
            }
            else
            {
                listaSup = _context.Supervisor.ToList();
                Supervisor s = new Supervisor();
                s.Id = 0;
                s.Nome = "Todos";
                listaSup.Add(s);
            }

            ViewData["ListaSupervisor"] = new SelectList(listaSup.OrderBy(s => s.Id), "Cpf", "Nome", pSupervisor);
            ViewData["anoCorrente"] = DateTime.Now.Year;

            if (pAno != null && pMes != null)
            {
                ViewData["anoCorrente"] = pAno;
                ViewData["mesCorrente"] = Convert.ToInt32(pMes);
                return View(buscarDados(Convert.ToInt32(pAno), Convert.ToInt32(pMes), (pSupervisor == null ? "0" : pSupervisor)));
            }

            return View(new List<RelatorioVendas>());
        }

        List<RelatorioVendas> buscarDados(int pAno, int pMes, string pSupervisor)
        {
            MySqlConnection con = new MySqlConnection();
            //con.ConnectionString = "Server=52.45.128.89;Port=3306;Database=tinglevendasdb;Uid=root;Pwd=Tingle@mysqlapi;Default Command Timeout=60";
            con.ConnectionString = _configuration.GetSection("ConnectionStrings").GetSection("tingleVendas").Value;
            //MySqlDataAdapter da = new MySqlDataAdapter("call tinglevendasdb.sp_recuperaVendas(2020, 3);", con);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("call tinglevendasdb.sp_recuperaVendas(" + pAno.ToString() + "," + pMes.ToString() + ",'" + pSupervisor.ToString() + "');", con);
            MySqlDataReader r = cmd.ExecuteReader();

            List<RelatorioVendas> l = new List<RelatorioVendas>();


            RelatorioVendas rv;

            if (r.HasRows)
            {
                while (r.Read())
                {
                    rv = new RelatorioVendas();
                    rv.NumPedido = Convert.ToString(r.GetValue(0));
                    rv.Cpf = Convert.ToString(r.GetValue(1));
                    rv.NomeCliente = Convert.ToString(r.GetValue(2));
                    rv.BovStatus = Convert.ToString(r.GetValue(3));
                    rv.TipoPedido = Convert.ToString(r.GetValue(4));
                    rv.Bairro = Convert.ToString(r.GetValue(5));
                    rv.QtdPedidosCancelados = Convert.ToInt32(r.GetValue(6));
                    rv.Ano = Convert.ToInt32(r.GetValue(7));
                    rv.Mes = Convert.ToInt32(r.GetValue(8));
                    rv.DataPedido = Convert.ToDateTime(r.GetValue(9));
                    rv.NomeSupervisor = Convert.ToString(r.GetValue(10));
                    rv.NomeVendedor = Convert.ToString(r.GetValue(11));
                    rv.Reinput = Convert.ToString(r.GetValue(12));

                    l.Add(rv);
                }
            }
            return l;
        }

        // GET: Oi360Transferencia/Details/5
        public async Task<IActionResult> Details(string id, int pAno, int pMes)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }
            ViewData["ano"] = pAno;
            ViewData["mes"] = pMes;

            var oi360Transferencia = await _context.Oi360Transferencia
                .Include(o => o.NumeroPedidoNavigation)
                .FirstOrDefaultAsync(m => m.NumeroPedido == id);
            if (oi360Transferencia == null)
            {
                return NotFound();
            }

            return View(oi360Transferencia);
        }

        // GET: Oi360Transferencia/Create
        public IActionResult Create(string id, int pAno, int pMes, string pStatusAtual)
        {
            setaDadosSessao();
            ViewData["ano"] = pAno;
            ViewData["mes"] = pMes;
            ViewData["dataAtual"] = DateTime.Now;
            ViewData["movimento"] = "Transferido de " + pStatusAtual + " para Perdas"; 
            ViewData["NumeroPedido"] = new SelectList(_context.Oi360, "NumeroDoPedido", "NumeroDoPedido",id);
            return View();
        }

        // POST: Oi360Transferencia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroPedido,Status,Observacao,UsuarioNome,DataMovimento,Movimento,Ativo")] Oi360Transferencia oi360Transferencia, int pano, int pmes)
        {
            setaDadosSessao();
            if (ModelState.IsValid)
            {
                //oi360Transferencia.DataMovimento = DateTime.Now;
                //oi360Transferencia.UsuarioNome = Convert.ToString(ViewData["nome"]);
                //oi360Transferencia.Movimento = "Transferido para perdas";
                _context.Add(oi360Transferencia);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Oi360Transferencia", new { pAno = pano, pMes = pmes });
            }
            ViewData["NumeroPedido"] = new SelectList(_context.Oi360, "NumeroDoPedido", "NomeCliente", oi360Transferencia.NumeroPedido);
            
            return View(oi360Transferencia);
        }

        // GET: Oi360Transferencia/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oi360Transferencia = await _context.Oi360Transferencia.FindAsync(id);
            if (oi360Transferencia == null)
            {
                return NotFound();
            }
            ViewData["NumeroPedido"] = new SelectList(_context.Oi360, "NumeroDoPedido", "NumeroDoPedido", oi360Transferencia.NumeroPedido);
            return View(oi360Transferencia);
        }

        // POST: Oi360Transferencia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumeroPedido,Status,Observacao,UsuarioNome,DataMovimento,Movimento,Ativo")] Oi360Transferencia oi360Transferencia)
        {
            if (id != oi360Transferencia.NumeroPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oi360Transferencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Oi360TransferenciaExists(oi360Transferencia.NumeroPedido))
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
            ViewData["NumeroPedido"] = new SelectList(_context.Oi360, "NumeroDoPedido", "NumeroDoPedido", oi360Transferencia.NumeroPedido);
            return View(oi360Transferencia);
        }

        // GET: Oi360Transferencia/Delete/5
        public async Task<IActionResult> Delete(string id,int pAno, int pMes)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["ano"] = pAno;
            ViewData["mes"] = pMes;
            var oi360Transferencia = await _context.Oi360Transferencia
                .Include(o => o.NumeroPedidoNavigation)
                .FirstOrDefaultAsync(m => m.NumeroPedido == id);
            if (oi360Transferencia == null)
            {
                return NotFound();
            }

            return View(oi360Transferencia);
        }

        // POST: Oi360Transferencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id, int pano, int pmes)
        {
            var oi360Transferencia = await _context.Oi360Transferencia.FindAsync(id);
            _context.Oi360Transferencia.Remove(oi360Transferencia);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Oi360Transferencia", new { pAno = pano, pMes = pmes });
        }

        private bool Oi360TransferenciaExists(string id)
        {
            return _context.Oi360Transferencia.Any(e => e.NumeroPedido == id);
        }

        public List<SelectListItem> Meses { get; } = new List<SelectListItem>
        {
          new SelectListItem { Value = "1", Text = "Janeiro"},
          new SelectListItem { Value = "2", Text = "Fevereiro" },
          new SelectListItem { Value = "3", Text = "Março" },
          new SelectListItem { Value = "4", Text = "Abril" },
          new SelectListItem { Value = "5", Text = "Maio" },
          new SelectListItem { Value = "6", Text = "Junho" },
          new SelectListItem { Value = "7", Text = "Julho" },
          new SelectListItem { Value = "8", Text = "Agosto" },
          new SelectListItem { Value = "9", Text = "Setembro" },
          new SelectListItem { Value = "10", Text = "Outubro" },
          new SelectListItem { Value = "11", Text = "Novembro" },
          new SelectListItem { Value = "12", Text = "Dezembro" }
        };
    }

}
