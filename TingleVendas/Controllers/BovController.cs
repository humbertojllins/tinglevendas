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
    public class BovController : Controller
    {
        private readonly TingleVendasContext _context;

        public BovController(TingleVendasContext context)
        {
            _context = context;
        }

        // GET: Bov
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bov.ToListAsync());
        }

        // GET: Bov/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bov = await _context.Bov
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bov == null)
            {
                return NotFound();
            }

            return View(bov);
        }

        // GET: Bov/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bov/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoSapPdv,GrupoUnidade,NumeroPedido,Produto,Tipo,DataPedido,BovStatus,DataStatus,MotivoRetirada,NumIdentidade,NomeCliente,SegmentoMercado,TelefoneContato,MetodoPagamento,DiaVencimentoFatura,DataCorte,TipoLogradouroInstalacao,NomeLogradouroInstalacao,NumeroInstalacao,TipoCompInstalacao,NumComp1Instalacao,NumComp2Instalacao,NumComp3Instalacao,BairroInstalacao,MunicipioInstalacao,EstadoInstalacao,CepInstalacao,NumLocalidade,CodigoEstacao,IdUnicoAcesso,IdAcessoAssociado,NumeroContrato,AcessoGpon,DddFixo,NumeroFixo,ClassePlano,Velocidade,QtdPontosAdicionaisTv,Tecnologia,LinhaProduto,Plano,PlanoAnterior,NomeCampanha,NomeOferta,NomeCampanhaAnterior,NomeOfertaAnterior,NomeCampanhaOriginal,NomeOfertaOriginal,CodigoSap,PontoVenda,TipoCanal,LoginVendedor,PedidoCriadoPor,AdicaoPonto,NumeroAtivoPedido,NumeroDocumento,TipoPedido,ClasseProduto,QtdTotalPontos,InteressePortabilidade,TipoPromocao,IdBundle,DtAbertura,CriadoEm,ModificadoEm,IndCombo,PlanoBundle,TipoPosseCombo,CdCampMovelTit,CampMovelTit,CdOftMovelTit,SitBundle,TipoRede,DescricaoCanalBov,DtFechamento,Evento,BundleTipoEventoVenda,BundleSitFixo,BundleSitVlx,BundleSitTv,BundleSitMovelTit,FlgVendaValida,BundleProdutoNovo,UnidadeNegocio,TpRetirada,BundleMaMovelTit,BundleNumeroDocumento,BundleContratoTv,BundleNumeroDocumentoTv,FlgMigCobreFixo,FlgMigCobreVelox,FlgMigTv,LoginOit")] Bov bov)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bov);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bov);
        }

        // GET: Bov/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bov = await _context.Bov.FindAsync(id);
            if (bov == null)
            {
                return NotFound();
            }
            return View(bov);
        }

        // POST: Bov/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoSapPdv,GrupoUnidade,NumeroPedido,Produto,Tipo,DataPedido,BovStatus,DataStatus,MotivoRetirada,NumIdentidade,NomeCliente,SegmentoMercado,TelefoneContato,MetodoPagamento,DiaVencimentoFatura,DataCorte,TipoLogradouroInstalacao,NomeLogradouroInstalacao,NumeroInstalacao,TipoCompInstalacao,NumComp1Instalacao,NumComp2Instalacao,NumComp3Instalacao,BairroInstalacao,MunicipioInstalacao,EstadoInstalacao,CepInstalacao,NumLocalidade,CodigoEstacao,IdUnicoAcesso,IdAcessoAssociado,NumeroContrato,AcessoGpon,DddFixo,NumeroFixo,ClassePlano,Velocidade,QtdPontosAdicionaisTv,Tecnologia,LinhaProduto,Plano,PlanoAnterior,NomeCampanha,NomeOferta,NomeCampanhaAnterior,NomeOfertaAnterior,NomeCampanhaOriginal,NomeOfertaOriginal,CodigoSap,PontoVenda,TipoCanal,LoginVendedor,PedidoCriadoPor,AdicaoPonto,NumeroAtivoPedido,NumeroDocumento,TipoPedido,ClasseProduto,QtdTotalPontos,InteressePortabilidade,TipoPromocao,IdBundle,DtAbertura,CriadoEm,ModificadoEm,IndCombo,PlanoBundle,TipoPosseCombo,CdCampMovelTit,CampMovelTit,CdOftMovelTit,SitBundle,TipoRede,DescricaoCanalBov,DtFechamento,Evento,BundleTipoEventoVenda,BundleSitFixo,BundleSitVlx,BundleSitTv,BundleSitMovelTit,FlgVendaValida,BundleProdutoNovo,UnidadeNegocio,TpRetirada,BundleMaMovelTit,BundleNumeroDocumento,BundleContratoTv,BundleNumeroDocumentoTv,FlgMigCobreFixo,FlgMigCobreVelox,FlgMigTv,LoginOit")] Bov bov)
        {
            if (id != bov.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bov);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BovExists(bov.Id))
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
            return View(bov);
        }

        // GET: Bov/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bov = await _context.Bov
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bov == null)
            {
                return NotFound();
            }

            return View(bov);
        }

        // POST: Bov/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bov = await _context.Bov.FindAsync(id);
            _context.Bov.Remove(bov);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BovExists(int id)
        {
            return _context.Bov.Any(e => e.Id == id);
        }
    }
}
