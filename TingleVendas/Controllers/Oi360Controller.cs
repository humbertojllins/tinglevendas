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
    public class Oi360Controller : Controller
    {
        private readonly TingleVendasContext _context;
        List<string> sistEntrada = new List<string> { "Ezconnect", "Oi360", "Br pronto" };
        List<string> tipoVenda = new List<string> { "Própria", "Contato", "Indicação" };
        List<string> genero = new List<string> { "Masculino", "Feminino"};
        List<string> portabilidade = new List<string> { "Sim", "Não" };
        List<string> bandaLargaTec = new List<string> { "Play", "Tv", "Cnpj", "Upgrade"};
        List<string> bandaLargaVeloc = new List<string> { "200", "400", "500"};
        List<string> tvPlanoTv = new List<string> { "Smart", "Avançado", "Top" };
        List<string> formaPag = new List<string> { "Débito", "Boleto", "Cartão" };
        List<string> dataVenc = new List<string> { "5", "10", "15", "20" };
        List<string> periodoAgendamento = new List<string> { "Manhã", "Tarde", "Todos"};
        



        public Oi360Controller(TingleVendasContext context)
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
                ViewData["grupo"] = usuario.IdGrupo;
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

        // GET: Oi360
        public async Task<IActionResult> Index(int? pAno, int? pMes)
        {
            DateTime dtInicial, dtFinal;

            
            setaDadosSessao();
            ViewData["ListaMeses"] = new SelectList(Meses, "Value", "Text", pMes);
            ViewData["anoCorrente"] = DateTime.Now.Year;

            if (pAno != null && pMes != null)
            {

                dtInicial = new DateTime(Convert.ToInt32(pAno), Convert.ToInt32(pMes), 1);
                dtFinal = dtInicial.AddMonths(1);

                ViewData["anoCorrente"] = pAno;
                ViewData["mesCorrente"] = Convert.ToInt32(pMes);
                //return View(buscarDados(Convert.ToInt32(pAno), Convert.ToInt32(pMes)));
                try
                {
                    List<Oi360> r = await _context.Oi360
                                                //.Include("IdsupervisorexternoNavigation")
                                                //.Include("IdsupervisorinternoNavigation")
                                                //.Include("IdvendedorexternoNavigation")
                                                //.Include("IdvendedorinternoNavigation")
                                                //.Include("Oi360Detalhes")
                                                .Where(a => a.DataEmQueOPedidoFoiRealizado >= dtInicial && a.DataEmQueOPedidoFoiRealizado < dtFinal).ToListAsync();
                    return View(r);
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    throw ex;
                }
                catch (System.InvalidCastException ex)
                {
                    foreach (System.Collections.DictionaryEntry de in ex.Data)
                    {

                    }
                }
                catch (System.InvalidOperationException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            //return View(await _context.Oi360.Include(det=>det.Oi360Detalhes).ToListAsync());
            List<Oi360> l = new List<Oi360>();
            return View(l);
            //var tingleVendasContext = _context.Supervisor.Include(s => s.IdCoordenadorNavigation);
            //_context.Oi360.First().Oi360Detalhes
        }

        // GET: Oi360/Details/5
        public async Task<IActionResult> Details(string id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var oi360 = await _context.Oi360
                .FirstOrDefaultAsync(m => m.NumeroDoPedido == id);
            if (oi360 == null)
            {
                return NotFound();
            }

            return View(oi360);
        }

        // GET: Oi360/Create
        public IActionResult Create()
        {
            setaDadosSessao();
           
            ViewData["SistEntrada"] = new SelectList(sistEntrada);
            ViewData["TipoVenda"] = new SelectList(tipoVenda);
            ViewData["Genero"] = new SelectList(genero);
            ViewData["Portabilidade"] = new SelectList(portabilidade);
            ViewData["BandaLargaTec"] = new SelectList(bandaLargaTec);
            ViewData["BandaLargaVeloc"] = new SelectList(bandaLargaVeloc);
            ViewData["TvPlanoTv"] = new SelectList(tvPlanoTv);
            ViewData["FormaPag"] = new SelectList(formaPag);
            ViewData["DataVenc"] = new SelectList(dataVenc);
            ViewData["PeriodoAgendamento"] = new SelectList(periodoAgendamento);
            ViewData["UF"] = new SelectList(_context.Uf, "Sigla", "Descricao");
            ViewData["Municipio"] = new SelectList(_context.Municipio.Where(m => m.Ativo==1 && m.Uf=="MA"), "Municipio1", "Municipio1");

            //Carrega os combos de supervisor
            List<Supervisor> lst = _context.Supervisor.Where(m => m.Interno == true).ToList();
            lst.Add(new Supervisor { Id = 0, Nome = "" });
            SelectList listaSupInt = new SelectList(lst);
            ViewData["SupInterno"] = new SelectList(lst.OrderBy(vi=>vi.Id), "Id", "Nome");

            lst = _context.Supervisor.Where(m => m.Interno == false).ToList();
            lst.Add(new Supervisor { Id = 0, Nome = "" });
            ViewData["SupExterno"] = new SelectList(lst.OrderBy(vi => vi.Id), "Id", "Nome");
            //Carrega os combos de supervisor

            int idSupInt = Convert.ToInt32(((SelectList)ViewData["SupInterno"]).FirstOrDefault().Value);
            int idSupExt = Convert.ToInt32(((SelectList)ViewData["SupExterno"]).FirstOrDefault().Value);


            //Carrega os combos de vendedor
            List<Vendedor> lstVend = _context.Vendedor.Where(m => m.Interno == true && m.IdSupervisor == idSupInt).ToList();
            lstVend.Add(new Vendedor { Id = 0, Nome = "" });
            ViewData["VendInterno"] = new SelectList(lstVend.OrderBy(vi => vi.Id), "Id", "Nome");

            lstVend = _context.Vendedor.Where(m => m.Interno == false && m.IdSupervisor == idSupExt).ToList();
            lstVend.Add(new Vendedor { Id = 0, Nome = "" });

            ViewData["VendExterno"] = new SelectList(lstVend.OrderBy(vi => vi.Id), "Id", "Nome");
            //Carrega os combos de vendedor

            return View();
        }

        void carregaCombos()
        {
            ViewData["SistEntrada"] = new SelectList(sistEntrada);
            ViewData["TipoVenda"] = new SelectList(tipoVenda);
            ViewData["Genero"] = new SelectList(genero);
            ViewData["Portabilidade"] = new SelectList(portabilidade);
            ViewData["BandaLargaTec"] = new SelectList(bandaLargaTec);
            ViewData["BandaLargaVeloc"] = new SelectList(bandaLargaVeloc);
            ViewData["TvPlanoTv"] = new SelectList(tvPlanoTv);
            ViewData["FormaPag"] = new SelectList(formaPag);
            ViewData["DataVenc"] = new SelectList(dataVenc);
            ViewData["PeriodoAgendamento"] = new SelectList(periodoAgendamento);
            ViewData["UF"] = new SelectList(_context.Uf, "Sigla", "Descricao");
            ViewData["Municipio"] = new SelectList(_context.Municipio.Where(m => m.Ativo == 1 && m.Uf == "MA"), "Municipio1", "Municipio1");

            //Carrega os combos de supervisor
            List<Supervisor> lst = _context.Supervisor.Where(m => m.Interno == true).ToList();
            lst.Add(new Supervisor { Id = 0, Nome = "" });
            SelectList listaSupInt = new SelectList(lst);
            ViewData["SupInterno"] = new SelectList(lst.OrderBy(vi => vi.Id), "Id", "Nome");

            lst = _context.Supervisor.Where(m => m.Interno == false).ToList();
            lst.Add(new Supervisor { Id = 0, Nome = "" });
            ViewData["SupExterno"] = new SelectList(lst.OrderBy(vi => vi.Id), "Id", "Nome");
            //Carrega os combos de supervisor

            int idSupInt = Convert.ToInt32(((SelectList)ViewData["SupInterno"]).FirstOrDefault().Value);
            int idSupExt = Convert.ToInt32(((SelectList)ViewData["SupExterno"]).FirstOrDefault().Value);


            //Carrega os combos de vendedor
            List<Vendedor> lstVend = _context.Vendedor.Where(m => m.Interno == true && m.IdSupervisor == idSupInt).ToList();
            lstVend.Add(new Vendedor { Id = 0, Nome = "" });
            ViewData["VendInterno"] = new SelectList(lstVend.OrderBy(vi => vi.Id), "Id", "Nome");

            lstVend = _context.Vendedor.Where(m => m.Interno == false && m.IdSupervisor == idSupExt).ToList();
            lstVend.Add(new Vendedor { Id = 0, Nome = "" });

            ViewData["VendExterno"] = new SelectList(lstVend.OrderBy(vi => vi.Id), "Id", "Nome");
            //Carrega os combos de vendedor

        }

        // POST: Oi360/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NumeroDoPedido,DataEmQueOPedidoFoiRealizado,HoraEmQueOPedidoFoiRealizado,DataEmQueOPedidoFoiTratado,HoraEmQueOPedidoFoiTratado,NomeCliente,Genero,DataDeNascimento,Cpf,Rg,RgOExpedidor,RgDataExpedicao,NomeCompletoDaMae,Nacionalidade,ContatoPrincipal,ContatoPrincipalWhatsapp,ContatoSecundario,ContatoSecundarioWhatsapp,Contato1Nome,Contato1Numero,Contato2Nome,Contato2Numero,Contato3Nome,Contato3Numero,EMail,MatriculaVendedor,NomeVendedor,FixoOi,FixoPortabilidade,FixoOperadora,FixoNumero,BandaLargaTecnologia,BandaLargaVelocidade,BandaLargaDataAgendamento,PeriodoAgendamentoFixoBandaLarga,OsBandaLargaFixo,TvPlanoTv,TvPontosAdicionais,TvCanaisALaCarte,TvOs,TvDataAgendamento,TvPeriodoAgendamento,TvIdBundle,ComboContratado,PagamentoFormaDePagamento,PagamentoVencimento,PagamentoContaOnline,PagamentoBanco,PagamentoAgencia,PagamentoConta,PagamentoDigito,StatusPrimario,StatusSecundario,ObservacaoVendedor,ObservacaoOperador,Operador,DataInicioTratamentoPedido,HoraInicioTratamentoPedido,DataTerminoTratamentoPedido,HoraTerminoTratamentoPedido,InstalacaoCep,InstalacaoLogradouro,InstalacaoNumero,InstalacaoBairro,InstalacaoCidade,InstalacaoEstado,InstalacaoReferencia,InstalacaoCdoeCdoi,InstalacaoComplemento1Tipo,InstalacaoComplemento1,InstalacaoComplemento2Tipo,InstalacaoComplemento2,InstalacaoComplemento3Tipo,InstalacaoComplemento3,CobrancaCep,CobrancaLogradouro,CobrancaNumero,CobrancaBairro,CobrancaCidade,CobrancaEstado,CobrancaComplemento1Tipo,CobrancaComplemento1,CobrancaComplemento2Tipo,CobrancaComplemento2,CobrancaComplemento3Tipo,CobrancaComplemento3,VendaMesAnterior,Reinput,TipoVenda,Idsupervisorinterno,Idvendedorinterno,Idsupervisorexterno,Idvendedorexterno")] Oi360 oi360)
        {
            setaDadosSessao();
            if (ModelState.IsValid)
            {
                DateTime dtAux1 = Convert.ToDateTime(Convert.ToDateTime(oi360.DataEmQueOPedidoFoiRealizado).ToString("yyyy-MM-dd"));
                DateTime dtAux2 = Convert.ToDateTime(Convert.ToDateTime(oi360.DataEmQueOPedidoFoiTratado).ToString("yyyy-MM-dd"));
                //Oi360 aux = oi360;
                oi360.DataEmQueOPedidoFoiRealizado = dtAux1;
                oi360.DataEmQueOPedidoFoiTratado = dtAux2;
                oi360.NomeCliente = oi360.NomeCliente.ToUpper();
                oi360.Datacadastro = DateTime.Now;
                oi360.Usuariocadastro = ViewData["nome"].ToString();
                oi360.Idsupervisorinterno = oi360.Idsupervisorinterno == 0 ? null : oi360.Idsupervisorinterno;
                oi360.Idvendedorinterno = oi360.Idvendedorinterno == 0 ? null : oi360.Idvendedorinterno;
                oi360.Idsupervisorexterno = oi360.Idsupervisorexterno == 0 ? null : oi360.Idsupervisorexterno;
                oi360.Idvendedorexterno = oi360.Idvendedorexterno == 0 ? null : oi360.Idvendedorexterno;

                _context.Add(oi360);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { pAno = Convert.ToDateTime(oi360.DataEmQueOPedidoFoiRealizado).Year, pMes = Convert.ToDateTime(oi360.DataEmQueOPedidoFoiRealizado).Month });
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            }
            carregaCombos();
            ViewData["UF"] = new SelectList(_context.Uf, "Sigla", "Descricao", oi360.InstalacaoEstado);
            ViewData["Municipio"] = new SelectList(_context.Municipio.Where(m => m.Ativo == 1 && m.Uf == "MA"), "Municipio1", "Municipio1", oi360.InstalacaoCidade);

            ViewData["SupInterno"] = new SelectList(_context.Supervisor.Where(m => m.Interno == true), "Id", "Nome", oi360.Idsupervisorinterno);
            ViewData["SupExterno"] = new SelectList(_context.Supervisor.Where(m => m.Interno == false), "Id", "Nome", oi360.Idsupervisorexterno);
            ViewData["VendInterno"] = new SelectList(_context.Vendedor.Where(m => m.Interno == true && m.IdSupervisor == oi360.Idsupervisorinterno), "Id", "Nome", oi360.Idvendedorinterno);
            ViewData["VendExterno"] = new SelectList(_context.Vendedor.Where(m => m.Interno == false && m.IdSupervisor == oi360.Idsupervisorexterno), "Id", "Nome", oi360.Idvendedorexterno);



            return View(oi360);
        }

        // GET: Oi360/Edit/5
        public async Task<IActionResult> Edit(string id, DateTime data)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var oi360 = await _context.Oi360.FindAsync(id);
            if (oi360 == null)
            {
                return NotFound();
            }
            ViewData["SistEntrada"] = new SelectList(sistEntrada, oi360.HoraEmQueOPedidoFoiTratado);
            ViewData["TipoVenda"] = new SelectList(tipoVenda, oi360.NomeCompletoDaMae);
            ViewData["Genero"] = new SelectList(genero, oi360.Nacionalidade);
            ViewData["Portabilidade"] = new SelectList(portabilidade, oi360.FixoPortabilidade);
            ViewData["BandaLargaTec"] = new SelectList(bandaLargaTec, oi360.BandaLargaTecnologia);
            ViewData["BandaLargaVeloc"] = new SelectList(bandaLargaVeloc, oi360.BandaLargaVelocidade);
            ViewData["TvPlanoTv"] = new SelectList(tvPlanoTv,oi360.TvPlanoTv);
            ViewData["FormaPag"] = new SelectList(formaPag, oi360.PagamentoFormaDePagamento);
            ViewData["DataVenc"] = new SelectList(dataVenc, oi360.PagamentoVencimento);
            ViewData["PeriodoAgendamento"] = new SelectList(periodoAgendamento, oi360.PeriodoAgendamentoFixoBandaLarga);
            ViewData["UF"] = new SelectList(_context.Uf, "Sigla", "Descricao", oi360.InstalacaoEstado);
            ViewData["Municipio"] = new SelectList(_context.Municipio.Where(m => m.Ativo == 1 && m.Uf == oi360.InstalacaoEstado), "Municipio1", "Municipio1",oi360.InstalacaoCidade);

            //Carrega os combos de supervisor
            List<Supervisor> lst = _context.Supervisor.Where(m => m.Interno == true).ToList();
            lst.Add(new Supervisor { Id = 0, Nome = "" });
            SelectList listaSupInt = new SelectList(lst);
            ViewData["SupInterno"] = new SelectList(lst.OrderBy(vi => vi.Id), "Id", "Nome", oi360.Idsupervisorinterno);

            lst = _context.Supervisor.Where(m => m.Interno == false).ToList();
            lst.Add(new Supervisor { Id = 0, Nome = "" });
            ViewData["SupExterno"] = new SelectList(lst.OrderBy(vi => vi.Id), "Id", "Nome", oi360.Idsupervisorexterno);
            //Carrega os combos de supervisor

            //ViewData["SupInterno"] = new SelectList(_context.Supervisor.Where(m => m.Interno == true), "Id", "Nome", oi360.Idsupervisorinterno);
            //ViewData["SupExterno"] = new SelectList(_context.Supervisor.Where(m => m.Interno == false), "Id", "Nome", oi360.Idsupervisorexterno);

            //ViewData["VendInterno"] = new SelectList(_context.Vendedor.Where(m => m.Interno == true && m.IdSupervisor == oi360.Idsupervisorinterno), "Id", "Nome", oi360.Idvendedorinterno);
            //ViewData["VendExterno"] = new SelectList(_context.Vendedor.Where(m => m.Interno == false && m.IdSupervisor == oi360.Idsupervisorexterno), "Id", "Nome", oi360.Idvendedorexterno);

            //Carrega os combos de vendedor
            List<Vendedor> lstVend = _context.Vendedor.Where(m => m.Interno == true && m.IdSupervisor == oi360.Idsupervisorinterno).ToList();
            lstVend.Add(new Vendedor { Id = 0, Nome = "" });
            ViewData["VendInterno"] = new SelectList(lstVend.OrderBy(vi => vi.Id), "Id", "Nome", oi360.Idvendedorinterno);

            lstVend = _context.Vendedor.Where(m => m.Interno == false && m.IdSupervisor == oi360.Idsupervisorexterno).ToList();
            lstVend.Add(new Vendedor { Id = 0, Nome = "" });

            ViewData["VendExterno"] = new SelectList(lstVend.OrderBy(vi => vi.Id), "Id", "Nome", oi360.Idvendedorexterno);
            //Carrega os combos de vendedor


            return View(oi360);
        }

        // POST: Oi360/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NumeroDoPedido,DataEmQueOPedidoFoiRealizado,HoraEmQueOPedidoFoiRealizado,DataEmQueOPedidoFoiTratado,HoraEmQueOPedidoFoiTratado,NomeCliente,Genero,DataDeNascimento,Cpf,Rg,RgOExpedidor,RgDataExpedicao,NomeCompletoDaMae,Nacionalidade,ContatoPrincipal,ContatoPrincipalWhatsapp,ContatoSecundario,ContatoSecundarioWhatsapp,Contato1Nome,Contato1Numero,Contato2Nome,Contato2Numero,Contato3Nome,Contato3Numero,EMail,MatriculaVendedor,NomeVendedor,FixoOi,FixoPortabilidade,FixoOperadora,FixoNumero,BandaLargaTecnologia,BandaLargaVelocidade,BandaLargaDataAgendamento,PeriodoAgendamentoFixoBandaLarga,OsBandaLargaFixo,TvPlanoTv,TvPontosAdicionais,TvCanaisALaCarte,TvOs,TvDataAgendamento,TvPeriodoAgendamento,TvIdBundle,ComboContratado,PagamentoFormaDePagamento,PagamentoVencimento,PagamentoContaOnline,PagamentoBanco,PagamentoAgencia,PagamentoConta,PagamentoDigito,StatusPrimario,StatusSecundario,ObservacaoVendedor,ObservacaoOperador,Operador,DataInicioTratamentoPedido,HoraInicioTratamentoPedido,DataTerminoTratamentoPedido,HoraTerminoTratamentoPedido,InstalacaoCep,InstalacaoLogradouro,InstalacaoNumero,InstalacaoBairro,InstalacaoCidade,InstalacaoEstado,InstalacaoReferencia,InstalacaoCdoeCdoi,InstalacaoComplemento1Tipo,InstalacaoComplemento1,InstalacaoComplemento2Tipo,InstalacaoComplemento2,InstalacaoComplemento3Tipo,InstalacaoComplemento3,CobrancaCep,CobrancaLogradouro,CobrancaNumero,CobrancaBairro,CobrancaCidade,CobrancaEstado,CobrancaComplemento1Tipo,CobrancaComplemento1,CobrancaComplemento2Tipo,CobrancaComplemento2,CobrancaComplemento3Tipo,CobrancaComplemento3,VendaMesAnterior,Reinput,TipoVenda,Idsupervisorinterno,Idvendedorinterno,Idsupervisorexterno,Idvendedorexterno")] Oi360 oi360)
        {
            setaDadosSessao();
            if (id != oi360.NumeroDoPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    oi360.Dataalteracao = DateTime.Now;
                    oi360.Usuarioalteracao = ViewData["nome"].ToString();
                    oi360.Idsupervisorinterno = oi360.Idsupervisorinterno == 0 ? null : oi360.Idsupervisorinterno;
                    oi360.Idvendedorinterno = oi360.Idvendedorinterno == 0 ? null : oi360.Idvendedorinterno;
                    oi360.Idsupervisorexterno = oi360.Idsupervisorexterno == 0 ? null : oi360.Idsupervisorexterno;
                    oi360.Idvendedorexterno = oi360.Idvendedorexterno == 0 ? null : oi360.Idvendedorexterno;

                    _context.Update(oi360);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Oi360Exists(oi360.NumeroDoPedido))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),new {pAno= Convert.ToDateTime(oi360.DataEmQueOPedidoFoiRealizado).Year , pMes = Convert.ToDateTime(oi360.DataEmQueOPedidoFoiRealizado).Month });
            }
            ViewData["UF"] = new SelectList(_context.Uf, "Sigla", "Descricao", oi360.InstalacaoEstado);
            ViewData["Municipio"] = new SelectList(_context.Municipio.Where(m => m.Ativo == 1 && m.Uf == "MA"), "Municipio1", "Municipio1", oi360.InstalacaoCidade);
            ViewData["SupInterno"] = new SelectList(_context.Supervisor.Where(m => m.Interno == true), "Id", "Nome", oi360.Idsupervisorinterno);
            ViewData["SupExterno"] = new SelectList(_context.Supervisor.Where(m => m.Interno == false), "Id", "Nome", oi360.Idsupervisorexterno);
            ViewData["VendInterno"] = new SelectList(_context.Vendedor.Where(m => m.Interno == true && m.IdSupervisor == oi360.Idsupervisorinterno), "Id", "Nome", oi360.Idvendedorinterno);
            ViewData["VendExterno"] = new SelectList(_context.Vendedor.Where(m => m.Interno == false && m.IdSupervisor == oi360.Idsupervisorexterno), "Id", "Nome", oi360.Idvendedorexterno);

            return View(oi360);
        }

        // GET: Oi360/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            setaDadosSessao();
            if (id == null)
            {
                return NotFound();
            }

            var oi360 = await _context.Oi360
                .FirstOrDefaultAsync(m => m.NumeroDoPedido == id);
            if (oi360 == null)
            {
                return NotFound();
            }

            return View(oi360);
        }

        // POST: Oi360/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oi360 = await _context.Oi360.FindAsync(id);
            _context.Oi360.Remove(oi360);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public ActionResult CarregaComboMunicipio(string uf)
        {
            ViewData["Municipio"] = new SelectList(_context.Municipio.Where(m => m.Ativo == 1 && m.Uf == uf), "IdMunicipio", "Municipio1");
            List<Municipio> listaMun = new List<Municipio>();
            Municipio mu;
            foreach (var item in ((SelectList)ViewData["Municipio"]))
            {
                mu = new Municipio();
                mu.Municipio1 = item.Text;
                mu.IdMunicipio = Convert.ToInt32(item.Value);
                listaMun.Add(mu);
                //ViewData[item.Value] = "Normal";
            }
            return Json(new { status = true, data = listaMun});

        }

        public ActionResult CarregaComboVendedor(int pInterno, int idSup)
        {
            //ViewData["VendInterno"] = new SelectList(_context.Vendedor.Where(m => m.Interno == 1 && m.IdSupervisor == idSup), "Id", "Nome");
            //ViewData["VendExterno"] = new SelectList(_context.Vendedor.Where(m => m.Interno == 0 && m.IdSupervisor == idSup), "Id", "Nome");
            bool pInternoBool = pInterno == 1 ? true : false;
            ViewData["Vendedores"] = new SelectList(_context.Vendedor.Where(m => m.Interno == pInternoBool && m.IdSupervisor == idSup), "Id", "Nome");

            //switch (pInterno)
            //{
            //    case 0:
            //        ViewData["Vendedores"]= ViewData["VendExterno"];
            //        break;
            //    case 1:
            //        ViewData["Vendedores"] = ViewData["VendInterno"];
            //        break;
            //}
            

            List<Vendedor> listaVend = new List<Vendedor>();
            Vendedor ve;
            foreach (var item in ((SelectList)ViewData["Vendedores"]))
            {
                ve = new Vendedor();
                ve.Nome = item.Text;
                ve.Id = Convert.ToInt32(item.Value);
                listaVend.Add(ve);
                //ViewData[item.Value] = "Normal";
            }
            return Json(new { status = true, data = listaVend });
        }

        public ActionResult ValidaDadosConta(string formaPagamento, string campoNome, string campoVal)
        {
            if (formaPagamento == "Débito")
            {
                if(campoVal is null || campoVal=="")
                    ModelState.AddModelError(campoNome, campoNome + "- Campo obrigatório");
            }
            return Json("teste");
        }


            private bool Oi360Exists(string id)
        {
            return _context.Oi360.Any(e => e.NumeroDoPedido == id);
        }

        // GET: Oi360Detalhes/Details/5
        public async Task<IActionResult> InfoAd(string id, int pAno, int pMes)
        {
            setaDadosSessao();
            ViewData["anoCorrente"] = pAno;
            ViewData["mesCorrente"] = pMes;
            if (id == null)
            {
                return NotFound();
            }

            //var oi360Detalhes = await _context.Oi360Detalhes
            //    .FirstOrDefaultAsync(m => m.NumeroPedido == id);
            //if (oi360Detalhes == null)
            //{
            //    return NotFound();
            //}
            return RedirectToAction("Details", "Oi360Detalhes", new { id = id, ano=pAno, mes=pMes });
            

            //return View(oi360Detalhes);
        }

        [HttpPost]
        public ActionResult CpfBairroExiste(string InstalacaoBairro, string Cpf,  string NumeroDoPedido)
        {
            bool cpfBairroExiste = false;
            try
            {
                if (NumeroDoPedido != "")
                {
                    cpfBairroExiste = _context.Oi360.Any(e => e.Cpf == Cpf && e.InstalacaoBairro== InstalacaoBairro && e.NumeroDoPedido != NumeroDoPedido);
                }
                else
                {
                    cpfBairroExiste = _context.Oi360.Any(e => e.Cpf == Cpf && e.InstalacaoBairro == InstalacaoBairro);
                }
                return Json(!cpfBairroExiste);
            }
            catch
            {
                return Json(false);
            }
        }

        //[HttpPost]
        //public ActionResult ValidaFormaPagamento(string PagamentoFormaDePagamento, string PagamentoBanco, string PagamentoAgencia, string PagamentoConta)
        //{
        //    try
        //    {
        //        if (PagamentoFormaDePagamento == "Débito")
        //        {
        //            if (PagamentoBanco is null)
        //                return Json(false);
        //            if (PagamentoAgencia is null)
        //                return Json(false);
        //            if (PagamentoConta is null)
        //                return Json(false);

        //        }
        //        return Json(true);
        //    }
        //    catch
        //    {
        //        return Json(false);
        //    }
        //}
       
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
