using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using TingleVendas.Models;

namespace TingleVendas.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly TingleVendasContext _context;
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        public HomeController(TingleVendasContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
           setaDadosSessao("");
            return View();
        }

        public IActionResult RelatorioQualidade(int? pAno, int? pMes)
        {
            ViewData["ListaMeses"] = new SelectList(Meses, "Value", "Text", pMes);
            ViewData["anoCorrente"] = DateTime.Now.Year;
            DateTime dtInicial, dtFinal;

            double qtdDetalhes, qtdCheckOk;
            if (pAno != null && pMes != null)
            {
                dtInicial = new DateTime(Convert.ToInt32(pAno), Convert.ToInt32(pMes), 1);
                dtFinal = dtInicial.AddMonths(1);

                ViewData["anoCorrente"] = pAno;
                ViewData["mesCorrente"] = Convert.ToInt32(pMes);

                ViewData["QtdVendas"]   = (from v in _context.Oi360
                                            where v.DataEmQueOPedidoFoiRealizado >= dtInicial
                                            where v.DataEmQueOPedidoFoiRealizado < dtFinal
                                           select v).Count();
                qtdDetalhes             = (from v in _context.Oi360
                                            join vd in _context.Oi360Detalhes on v.NumeroDoPedido equals vd.NumeroPedido
                                           where v.DataEmQueOPedidoFoiRealizado >= dtInicial
                                           where v.DataEmQueOPedidoFoiRealizado < dtFinal
                                           select vd).Count();

                ViewData["QtdInfoAdd"] = (qtdDetalhes == 0 ? 0 : Convert.ToInt32((qtdDetalhes / Convert.ToDouble(ViewData["QtdVendas"])) * 100));

                qtdCheckOk =              (from v in _context.Oi360
                                          join vd in _context.Oi360Detalhes on v.NumeroDoPedido equals vd.NumeroPedido
                                           where v.DataEmQueOPedidoFoiRealizado >= dtInicial
                                           where v.DataEmQueOPedidoFoiRealizado < dtFinal
                                           where vd.CheckListConfirmacaoClienteOk==true
                                          where vd.CheckListDocumentoClienteOk==true
                                          where vd.CheckListPropostaOk==true
                                          select vd).Count();
                ViewData["QtdCheckOk"] = (qtdCheckOk == 0 ? 0 : Convert.ToInt32((qtdCheckOk / qtdDetalhes) * 100));
                ViewData["QtdPendencias"] = ((int)ViewData["QtdVendas"]) - ((int)qtdDetalhes);
                ViewData["QtdReinput"] = (from v in _context.Oi360
                                          where v.DataEmQueOPedidoFoiRealizado >= dtInicial
                                          where v.DataEmQueOPedidoFoiRealizado < dtFinal
                                          where v.Reinput == true
                                          select v).Count();

            }
            else
            {
                ViewData["QtdVendas"] = 0;
                ViewData["QtdInfoAdd"] = 0;
                ViewData["QtdCheckOk"] = 0;
                ViewData["QtdReinput"] = 0;
            }


            setaDadosSessao("Relatório de qualidade das vendas");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*private bool setaDadosSessao()
        {
            var usuario = HttpContext.Session.GetObjectFromJson<Usuario>("sessionUsuario");

            if (usuario != null)
            {
                //ViewData["nome"] = usuario.Nome + " " + usuario.Sobrenome;
                ViewData["nome"] = usuario.Nome;
                ////ViewData["foto"] = usuario.Foto;
                ViewData["url"] = "Usuario";
                return true;
            }
            return false;
        }*/

        /// <summary>
        /// Seta os dados de sessao do usuario para ser utilizada na pagina _layout
        /// </summary>
        private void setaDadosSessao(string tela)
        {
            var usuario = HttpContext.Session.GetObjectFromJson<Usuario>("sessionUsuario");

            if (usuario != null)
            {
                ViewData["nome"] = usuario.Nome + " " + usuario.Sobrenome;
                ViewData["email"] = usuario.Email;
                //////ViewData["foto"] = usuario.Foto;
                ViewData["url"] = tela;
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
