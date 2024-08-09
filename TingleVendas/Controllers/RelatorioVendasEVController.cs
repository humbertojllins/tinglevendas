using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using TingleVendas.Models;

namespace TingleVendas.Controllers
{
    [Authorize]
    public class RelatorioVendasEVController : Controller
    {
        private readonly TingleVendasContext _context;

        private IConfiguration _configuration;
        
        public RelatorioVendasEVController(TingleVendasContext context, IConfiguration config)
        {
            _context = context;
            _configuration = config;
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
                ViewData["url"] = "Relatório de evolução das vendas";
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

        public async Task<IActionResult> Index()
        {
            setaDadosSessao();
            ViewData["anoCorrente"] = DateTime.Now.Year;
            
            return View();
        }

        [HttpGet]
        public JsonResult buscarDadosJSon(int pAno)
        {
            var lista = buscarDados(pAno);
            return Json(lista);
        }


        List<RelatorioVendasResumo> buscarDados(int pAno)
        {
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = _configuration.GetSection("ConnectionStrings").GetSection("tingleVendas").Value;
            //MySqlDataAdapter da = new MySqlDataAdapter("call tinglevendasdb.sp_recuperaVendas(2020, 3);", con);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("call tinglevendasdb.sp_recuperaVendas_Evolucao(" + pAno.ToString() +");", con);
            MySqlDataReader r = cmd.ExecuteReader();

            List<RelatorioVendasResumo> l = new List<RelatorioVendasResumo>();


            RelatorioVendasResumo rv;

            if (r.HasRows)
            {
                while (r.Read())
                {
                    rv = new RelatorioVendasResumo();

                    rv.Ano = Convert.ToInt32(r.GetValue(0));
                    rv.Mes = Convert.ToInt32(r.GetValue(1));
                    rv.qtdConcluido = Convert.ToInt32(r.GetValue(2));
                    rv.qtdPendente = Convert.ToInt32(r.GetValue(3));
                    rv.qtdCancelado = Convert.ToInt32(r.GetValue(4));
                    rv.qtdErro = Convert.ToInt32(r.GetValue(5));

                    l.Add(rv);
                }
            }

            return l;
        }

        void CarregaCombo()
        {
           
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
