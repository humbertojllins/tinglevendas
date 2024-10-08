﻿using System;
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
    public class RelatorioPendenciasController : Controller
    {
        private readonly TingleVendasContext _context;
        private IConfiguration _configuration;
        public RelatorioPendenciasController(TingleVendasContext context, IConfiguration config)
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
                ViewData["cpf"] = usuario.Cpf;
                ViewData["grupo"] = usuario.IdGrupo;
                ////ViewData["foto"] = usuario.Foto;
                ViewData["url"] = "Relatório de Pendências";
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

        public async Task<IActionResult> Index(int? pAno, int? pMes, string? pSupervisor)
        {
            setaDadosSessao();
            ViewData["ListaMeses"] = new SelectList(Meses, "Value", "Text", pMes);
            //List<Supervisor> listaSup;
            //if (ViewData["cpf"] != null && Convert.ToString(ViewData["grupo"]) == "6")
            //{
            //    listaSup = _context.Supervisor.Where(s => s.Cpf == Convert.ToString(ViewData["cpf"])).ToList();
            //}
            //else
            //{
            //    listaSup = _context.Supervisor.ToList();
            //    Supervisor s = new Supervisor();
            //    s.Id = 0;
            //    s.Nome = "Todos";
            //    listaSup.Add(s);
            //}

            //ViewData["ListaSupervisor"] = new SelectList(listaSup.OrderBy(s => s.Id), "Cpf", "Nome", pSupervisor);
            ViewData["anoCorrente"] = DateTime.Now.Year;

            if (pAno != null && pMes != null)
            {
                ViewData["anoCorrente"] = pAno;
                ViewData["mesCorrente"] = Convert.ToInt32(pMes);
                return View(buscarDados(Convert.ToInt32(pAno), Convert.ToInt32(pMes)));
            }
            //ViewData["QtdVendasReal"] = 0;
            //ViewData["QtdVendasInstalados"] = 0;
            //ViewData["QtdVendasPendentes"] = 0;
            //ViewData["QtdVendasCanceladas"] = 0;
            return View(new List<RelatorioPendencias>());
        }


        List<RelatorioPendencias> buscarDados(int pAno, int pMes)
        {

            MySqlConnection con = new MySqlConnection();
            //con.ConnectionString = "Server=52.45.128.89;Port=3306;Database=tinglevendasdb;Uid=root;Pwd=Tingle@mysqlapi;Default Command Timeout=60";
            con.ConnectionString = _configuration.GetSection("ConnectionStrings").GetSection("tingleVendas").Value;
            //MySqlDataAdapter da = new MySqlDataAdapter("call tinglevendasdb.sp_recuperaVendas(2020, 3);", con);
            con.Open();
            MySqlCommand cmd = new MySqlCommand("call tinglevendasdb.sp_recuperaPendencias(" + pAno.ToString() + "," + pMes.ToString() +  ");", con);
            MySqlDataReader r = cmd.ExecuteReader();

            List<RelatorioPendencias> l = new List<RelatorioPendencias>();


            RelatorioPendencias rv;

            if (r.HasRows)
            {
                while (r.Read())
                {
                    rv = new RelatorioPendencias();
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
                    rv.NomeSupervisorInterno = Convert.ToString(r.GetValue(10));
                    rv.NomeSupervisorExterno = Convert.ToString(r.GetValue(11));
                    rv.NomeVendedorInterno = Convert.ToString(r.GetValue(12));
                    rv.NomeVendedorExterno = Convert.ToString(r.GetValue(13));
                    rv.Status_Fechada = Convert.ToString(r.GetValue(14));
                    rv.Desc_Status = Convert.ToString(r.GetValue(15));
                    rv.qtd_atribuicoes = ConverterInteiro(Convert.ToString(r.GetValue(16)));
                    rv.agendamentofuturo = ConverterInteiro(Convert.ToString(r.GetValue(17)));
                    rv.ultimoAgendamento = ConverterData(Convert.ToString(r.GetValue(18)));
                    rv.status_pendencia = Convert.ToString(r.GetValue(19));
                    rv.observacaoPendencia = Convert.ToString(r.GetValue(20));

                    l.Add(rv);
                }
            }

            return l;
        }

        public int ConverterInteiro(string valor)
        {
            int vlrint = 0;
            int.TryParse(valor, out vlrint);

            return vlrint;
        }

        public DateTime ConverterData(string valor)
        {
            DateTime dt = new DateTime(9999, 12, 31);
            DateTime.TryParse(valor, out dt);

            return dt;
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
