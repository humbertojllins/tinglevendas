using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TingleVendas.Models
{
    public partial class TingleVendasContext : DbContext
    {
        public TingleVendasContext()
        {
        }

        public TingleVendasContext(DbContextOptions<TingleVendasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bov> Bov { get; set; }
        public virtual DbSet<CanalVendas> CanalVendas { get; set; }
        public virtual DbSet<Click> Click { get; set; }
        public virtual DbSet<Coordenador> Coordenador { get; set; }
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistory { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<GrupoMenu> GrupoMenu { get; set; }
        public virtual DbSet<LogImportacao> LogImportacao { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<Oi360> Oi360 { get; set; }
        public virtual DbSet<Oi360Detalhes> Oi360Detalhes { get; set; }
        public virtual DbSet<Oi360Transferencia> Oi360Transferencia { get; set; }
        public virtual DbSet<StatusOiVende> StatusOiVende { get; set; }
        public virtual DbSet<Supervisor> Supervisor { get; set; }
        public virtual DbSet<Uf> Uf { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Vendedor> Vendedor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=3.133.153.150;port=3306;database=tinglevendasdb;uid=root;pwd=Tingle@mysqlapi", x => x.ServerVersion("5.7.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bov>(entity =>
            {
                entity.ToTable("BOV");

                entity.HasIndex(e => e.BairroInstalacao)
                    .HasName("index_bairro_bov");

                entity.HasIndex(e => e.NomeCliente)
                    .HasName("index2_nm_bov");

                entity.HasIndex(e => e.NumeroDocumento)
                    .HasName("index_num_doc");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AcessoGpon)
                    .HasColumnName("ACESSO_GPON")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.AdicaoPonto)
                    .HasColumnName("ADICAO_PONTO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BairroInstalacao)
                    .HasColumnName("BAIRRO_INSTALACAO")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BovStatus)
                    .HasColumnName("BOV_STATUS")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BundleContratoTv)
                    .HasColumnName("BUNDLE_CONTRATO_TV")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BundleMaMovelTit)
                    .HasColumnName("BUNDLE_MA_MOVEL_TIT")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BundleNumeroDocumento)
                    .HasColumnName("BUNDLE_NUMERO_DOCUMENTO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BundleNumeroDocumentoTv)
                    .HasColumnName("BUNDLE_NUMERO_DOCUMENTO_TV")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BundleProdutoNovo)
                    .HasColumnName("BUNDLE_PRODUTO_NOVO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BundleSitFixo)
                    .HasColumnName("BUNDLE_SIT_FIXO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BundleSitMovelTit)
                    .HasColumnName("BUNDLE_SIT_MOVEL_TIT")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BundleSitTv)
                    .HasColumnName("BUNDLE_SIT_TV")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BundleSitVlx)
                    .HasColumnName("BUNDLE_SIT_VLX")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BundleTipoEventoVenda)
                    .HasColumnName("BUNDLE_TIPO_EVENTO_VENDA")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CampMovelTit)
                    .HasColumnName("CAMP_MOVEL_TIT")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CdCampMovelTit)
                    .HasColumnName("CD_CAMP_MOVEL_TIT")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CdOftMovelTit)
                    .HasColumnName("CD_OFT_MOVEL_TIT")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CepInstalacao)
                    .HasColumnName("CEP_INSTALACAO")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClassePlano)
                    .HasColumnName("CLASSE_PLANO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ClasseProduto)
                    .HasColumnName("CLASSE_PRODUTO")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CodigoEstacao)
                    .HasColumnName("CODIGO_ESTACAO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoSap)
                    .HasColumnName("CODIGO_SAP")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodigoSapPdv)
                    .HasColumnName("CODIGO_SAP_PDV")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CriadoEm)
                    .HasColumnName("CRIADO_EM")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataCorte)
                    .HasColumnName("DATA_CORTE")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataPedido)
                    .HasColumnName("DATA_PEDIDO")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataStatus)
                    .HasColumnName("DATA_STATUS")
                    .HasColumnType("datetime");

                entity.Property(e => e.DddFixo)
                    .HasColumnName("DDD_FIXO")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DescricaoCanalBov)
                    .HasColumnName("DESCRICAO_CANAL_BOV")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DiaVencimentoFatura)
                    .HasColumnName("DIA_VENCIMENTO_FATURA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DtAbertura)
                    .HasColumnName("DT_ABERTURA")
                    .HasColumnType("datetime");

                entity.Property(e => e.DtFechamento)
                    .HasColumnName("DT_FECHAMENTO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.EstadoInstalacao)
                    .HasColumnName("ESTADO_INSTALACAO")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Evento)
                    .HasColumnName("EVENTO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FlgMigCobreFixo)
                    .HasColumnName("FLG_MIG_COBRE_FIXO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FlgMigCobreVelox)
                    .HasColumnName("FLG_MIG_COBRE_VELOX")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FlgMigTv)
                    .HasColumnName("FLG_MIG_TV")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FlgVendaValida)
                    .HasColumnName("FLG_VENDA_VALIDA")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GrupoUnidade)
                    .HasColumnName("GRUPO_UNIDADE")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IdAcessoAssociado)
                    .HasColumnName("ID_ACESSO_ASSOCIADO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IdBundle)
                    .HasColumnName("ID_BUNDLE")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUnicoAcesso)
                    .HasColumnName("ID_UNICO_ACESSO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IndCombo)
                    .HasColumnName("IND_COMBO")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.InteressePortabilidade)
                    .HasColumnName("INTERESSE_PORTABILIDADE")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LinhaProduto)
                    .HasColumnName("LINHA_PRODUTO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LoginOit)
                    .HasColumnName("LOGIN_OIT")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.LoginVendedor)
                    .HasColumnName("LOGIN_VENDEDOR")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MetodoPagamento)
                    .HasColumnName("METODO_PAGAMENTO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ModificadoEm)
                    .HasColumnName("MODIFICADO_EM")
                    .HasColumnType("datetime");

                entity.Property(e => e.MotivoRetirada)
                    .HasColumnName("MOTIVO_RETIRADA")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MotivoSituacaoPedido)
                    .HasColumnName("MOTIVO_SITUACAO_PEDIDO")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.MunicipioInstalacao)
                    .HasColumnName("MUNICIPIO_INSTALACAO")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeCampanha)
                    .HasColumnName("NOME_CAMPANHA")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeCampanhaAnterior)
                    .HasColumnName("NOME_CAMPANHA_ANTERIOR")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeCampanhaOriginal)
                    .HasColumnName("NOME_CAMPANHA_ORIGINAL")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeCliente)
                    .HasColumnName("NOME_CLIENTE")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeLogradouroInstalacao)
                    .HasColumnName("NOME_LOGRADOURO_INSTALACAO")
                    .HasColumnType("varchar(120)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeOferta)
                    .HasColumnName("NOME_OFERTA")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeOfertaAnterior)
                    .HasColumnName("NOME_OFERTA_ANTERIOR")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeOfertaOriginal)
                    .HasColumnName("NOME_OFERTA_ORIGINAL")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumComp1Instalacao)
                    .HasColumnName("NUM_COMP1_INSTALACAO")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumComp2Instalacao)
                    .HasColumnName("NUM_COMP2_INSTALACAO")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumComp3Instalacao)
                    .HasColumnName("NUM_COMP3_INSTALACAO")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumIdentidade)
                    .HasColumnName("NUM_IDENTIDADE")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumLocalidade)
                    .HasColumnName("NUM_LOCALIDADE")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumeroAtivoPedido)
                    .HasColumnName("NUMERO_ATIVO_PEDIDO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroContrato)
                    .HasColumnName("NUMERO_CONTRATO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroDocumento)
                    .HasColumnName("NUMERO_DOCUMENTO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroFixo)
                    .HasColumnName("NUMERO_FIXO")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumeroInstalacao)
                    .HasColumnName("NUMERO_INSTALACAO")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumeroPedido)
                    .HasColumnName("NUMERO_PEDIDO")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PedidoCriadoPor)
                    .HasColumnName("PEDIDO_CRIADO_POR")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Plano)
                    .HasColumnName("PLANO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PlanoAnterior)
                    .HasColumnName("PLANO_ANTERIOR")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PlanoBundle)
                    .HasColumnName("PLANO_BUNDLE")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.PontoVenda)
                    .HasColumnName("PONTO_VENDA")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Produto)
                    .HasColumnName("PRODUTO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.QtdPontosAdicionaisTv)
                    .HasColumnName("QTD_PONTOS_ADICIONAIS_TV")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.QtdTotalPontos)
                    .HasColumnName("QTD_TOTAL_PONTOS")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SegmentoMercado)
                    .HasColumnName("SEGMENTO_MERCADO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.SitBundle)
                    .HasColumnName("SIT_BUNDLE")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Tecnologia)
                    .HasColumnName("TECNOLOGIA")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelefoneContato)
                    .HasColumnName("TELEFONE_CONTATO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Tipo)
                    .HasColumnName("TIPO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoCanal)
                    .HasColumnName("TIPO_CANAL")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoCompInstalacao)
                    .HasColumnName("TIPO_COMP_INSTALACAO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoLogradouroInstalacao)
                    .HasColumnName("TIPO_LOGRADOURO_INSTALACAO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoPedido)
                    .HasColumnName("TIPO_PEDIDO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoPosseCombo)
                    .HasColumnName("TIPO_POSSE_COMBO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoPromocao)
                    .HasColumnName("TIPO_PROMOCAO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoRede)
                    .HasColumnName("TIPO_REDE")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TpRetirada)
                    .HasColumnName("TP_RETIRADA")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UnidadeNegocio)
                    .HasColumnName("UNIDADE_NEGOCIO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Velocidade)
                    .HasColumnName("VELOCIDADE")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<CanalVendas>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descricao)
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Click>(entity =>
            {
                entity.ToTable("CLICK");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Agendado)
                    .HasColumnName("AGENDADO")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Atividade)
                    .HasColumnName("ATIVIDADE")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Bairro)
                    .HasColumnName("BAIRRO")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Cep)
                    .HasColumnName("CEP")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CodSap)
                    .HasColumnName("COD_SAP")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Codencerramento)
                    .HasColumnName("CODENCERRAMENTO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Compl)
                    .HasColumnName("COMPL")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Contato1)
                    .HasColumnName("CONTATO1")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Contato2)
                    .HasColumnName("CONTATO2")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Contato3)
                    .HasColumnName("CONTATO3")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Contatoefetivo)
                    .HasColumnName("CONTATOEFETIVO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Coordenador)
                    .HasColumnName("COORDENADOR")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.CpfCliente)
                    .HasColumnName("CPF_CLIENTE")
                    .HasColumnType("varchar(14)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DataCarga)
                    .HasColumnName("data_carga")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataFechamento)
                    .HasColumnName("DATA_FECHAMENTO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DataFimPriAgendamento)
                    .HasColumnName("DATA_FIM_PRI_AGENDAMENTO")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DataFimUltAgendamento)
                    .HasColumnName("DATA_FIM_ULT_AGENDAMENTO")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Ddd)
                    .HasColumnName("ddd")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DescStatus)
                    .HasColumnName("DESC_STATUS")
                    .HasColumnType("varchar(120)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Filial)
                    .HasColumnName("FILIAL")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.FilialIi)
                    .HasColumnName("FILIAL_II")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Fimagendamento)
                    .HasColumnName("FIMAGENDAMENTO")
                    .HasColumnType("datetime");

                entity.Property(e => e.Gc)
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GcCarteira)
                    .HasColumnName("GC_CARTEIRA")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Gra)
                    .HasColumnName("GRA")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Gram)
                    .HasColumnName("GRAM")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GrupoCanal)
                    .HasColumnName("GRUPO_CANAL")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.GrupoUnidade)
                    .HasColumnName("GRUPO_UNIDADE")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Gv)
                    .HasColumnName("GV")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IndVendaConjunta)
                    .HasColumnName("IND_VENDA_CONJUNTA")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Inicioagendamento)
                    .HasColumnName("INICIOAGENDAMENTO")
                    .HasColumnType("datetime");

                entity.Property(e => e.MatriculaVendedor)
                    .HasColumnName("MATRICULA_VENDEDOR")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Matriculatecnico)
                    .HasColumnName("MATRICULATECNICO")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeCliente)
                    .HasColumnName("NOME_CLIENTE")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeLogr)
                    .HasColumnName("NOME_LOGR")
                    .HasColumnType("varchar(120)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomeMunicipio)
                    .HasColumnName("NOME_MUNICIPIO")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NomePdv)
                    .HasColumnName("NOME_PDV")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Nrba)
                    .HasColumnName("NRBA")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumLoc)
                    .HasColumnName("NUM_LOC")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumOs)
                    .HasColumnName("NUM_OS")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NumPorta)
                    .HasColumnName("NUM_PORTA")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Prontoparaexecucao)
                    .HasColumnName("PRONTOPARAEXECUCAO")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.QtdReagendamentoClick)
                    .HasColumnName("QTD_REAGENDAMENTO_CLICK")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Safra)
                    .HasColumnName("SAFRA")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.StatusFechada)
                    .HasColumnName("STATUS_FECHADA")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Tecnico)
                    .HasColumnName("TECNICO")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TelCoord)
                    .HasColumnName("TEL_COORD")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoCompl)
                    .HasColumnName("TIPO_COMPL")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TipoLogr)
                    .HasColumnName("TIPO_LOGR")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Coordenador>(entity =>
            {
                entity.HasIndex(e => e.Cpf)
                    .HasName("Cpf_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cpf)
                    .HasColumnType("varchar(14)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Nome)
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId)
                    .HasColumnType("varchar(95)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasColumnType("varchar(32)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Grupo>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Descricao)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<GrupoMenu>(entity =>
            {
                entity.HasKey(e => new { e.IdGrupo, e.Idmenu })
                    .HasName("PRIMARY");

                entity.ToTable("Grupo_Menu");

                entity.HasIndex(e => e.IdGrupo)
                    .HasName("fkgrupo_idx");

                entity.HasIndex(e => e.Idmenu)
                    .HasName("fkmenu_idx");

                entity.Property(e => e.IdGrupo)
                    .HasColumnName("idGrupo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idmenu)
                    .HasColumnName("idmenu")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.GrupoMenu)
                    .HasForeignKey(d => d.IdGrupo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkgrupo");

                entity.HasOne(d => d.IdmenuNavigation)
                    .WithMany(p => p.GrupoMenu)
                    .HasForeignKey(d => d.Idmenu)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkmenu");
            });

            modelBuilder.Entity<LogImportacao>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Chave1)
                    .IsRequired()
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Chave2)
                    .HasColumnType("varchar(150)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Chave3)
                    .HasColumnType("varchar(200)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DataHora)
                    .IsRequired()
                    .HasColumnName("Data_Hora")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Erro)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Linha).HasColumnType("int(11)");

                entity.Property(e => e.Planilha)
                    .IsRequired()
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("char(1)")
                    .HasDefaultValueSql("'A'")
                    .HasComment("A-ATIVO\\nI-INATIVO")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Genre)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Price).HasColumnType("decimal(65,30)");

                entity.Property(e => e.Title)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.Uf)
                    .HasName("fkuf_idx");

                entity.Property(e => e.IdMunicipio)
                    .HasColumnName("idMunicipio")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Ativo)
                    .HasColumnName("ativo")
                    .HasColumnType("int(11)")
                    .HasComment("Indica se o municipio será usado no sistema");

                entity.Property(e => e.Municipio1)
                    .HasColumnName("municipio")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Uf)
                    .HasColumnName("uf")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.UfNavigation)
                    .WithMany(p => p.Municipio)
                    .HasForeignKey(d => d.Uf)
                    .HasConstraintName("fkuf");
            });

            modelBuilder.Entity<Oi360>(entity =>
            {
                entity.HasKey(e => e.NumeroDoPedido)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.Cpf)
                    .HasName("idx_cpf");

                entity.HasIndex(e => e.DataEmQueOPedidoFoiRealizado)
                    .HasName("index2");

                entity.HasIndex(e => e.Idsupervisorexterno)
                    .HasName("fk_sup_ext_idx");

                entity.HasIndex(e => e.Idsupervisorinterno)
                    .HasName("fk_sup_int_idx");

                entity.HasIndex(e => e.Idvendedorexterno)
                    .HasName("fk_vend_ext_idx");

                entity.HasIndex(e => e.Idvendedorinterno)
                    .HasName("fk_vend_int_idx");

                entity.HasIndex(e => e.InstalacaoBairro)
                    .HasName("idx_bairro_oi360");

                entity.HasIndex(e => e.NomeCliente)
                    .HasName("index3");

                entity.Property(e => e.NumeroDoPedido)
                    .HasColumnName("Numero_do_pedido")
                    .HasColumnType("varchar(18)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.BandaLargaDataAgendamento)
                    .HasColumnName("Banda_Larga_Data_agendamento")
                    .HasColumnType("datetime");

                entity.Property(e => e.BandaLargaTecnologia)
                    .HasColumnName("Banda_Larga_Tecnologia")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.BandaLargaVelocidade)
                    .HasColumnName("Banda_Larga_Velocidade")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaBairro)
                    .HasColumnName("Cobranca_Bairro")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaCep)
                    .HasColumnName("Cobranca_CEP")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaCidade)
                    .HasColumnName("Cobranca_Cidade")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaComplemento1)
                    .HasColumnName("Cobranca_Complemento_1")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaComplemento1Tipo)
                    .HasColumnName("Cobranca_Complemento_1_tipo")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaComplemento2)
                    .HasColumnName("Cobranca_Complemento_2")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaComplemento2Tipo)
                    .HasColumnName("Cobranca_Complemento_2_tipo")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaComplemento3)
                    .HasColumnName("Cobranca_Complemento_3")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaComplemento3Tipo)
                    .HasColumnName("Cobranca_Complemento_3_tipo")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaEstado)
                    .HasColumnName("Cobranca_Estado")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaLogradouro)
                    .HasColumnName("Cobranca_Logradouro")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.CobrancaNumero)
                    .HasColumnName("Cobranca_Numero")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ComboContratado)
                    .HasColumnName("Combo_contratado")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Contato1Nome)
                    .HasColumnName("Contato_1_nome")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Contato1Numero)
                    .HasColumnName("Contato_1_numero")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Contato2Nome)
                    .HasColumnName("Contato_2_nome")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Contato2Numero)
                    .HasColumnName("Contato_2_numero")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Contato3Nome)
                    .HasColumnName("Contato_3_nome")
                    .HasColumnType("varchar(21)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Contato3Numero)
                    .HasColumnName("Contato_3_numero")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContatoPrincipal)
                    .HasColumnName("Contato_principal")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContatoPrincipalWhatsapp)
                    .HasColumnName("Contato_principal_whatsapp")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContatoSecundario)
                    .HasColumnName("Contato_secundario")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ContatoSecundarioWhatsapp)
                    .HasColumnName("Contato_secundario_whatsapp")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Cpf)
                    .HasColumnName("CPF")
                    .HasColumnType("varchar(14)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DataDeNascimento)
                    .HasColumnName("Data_de_nascimento")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataEmQueOPedidoFoiRealizado)
                    .HasColumnName("Data_em_que_o_pedido_foi_realizado")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataEmQueOPedidoFoiTratado)
                    .HasColumnName("Data_em_que_o_pedido_foi_tratado")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataInicioTratamentoPedido)
                    .HasColumnName("Data_Inicio_Tratamento_Pedido")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataTerminoTratamentoPedido)
                    .HasColumnName("Data_Termino_Tratamento_Pedido")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dataalteracao)
                    .HasColumnName("dataalteracao")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datacadastro)
                    .HasColumnName("datacadastro")
                    .HasColumnType("datetime");

                entity.Property(e => e.EMail)
                    .HasColumnName("E_mail")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FixoNumero)
                    .HasColumnName("Fixo_Numero")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FixoOi)
                    .HasColumnName("Fixo_Oi")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FixoOperadora)
                    .HasColumnName("Fixo_Operadora")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.FixoPortabilidade)
                    .HasColumnName("Fixo_Portabilidade")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Genero)
                    .HasColumnType("varchar(9)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HoraEmQueOPedidoFoiRealizado)
                    .HasColumnName("Hora_em_que_o_pedido_foi_realizado")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HoraEmQueOPedidoFoiTratado)
                    .HasColumnName("Hora_em_que_o_pedido_foi_tratado")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HoraInicioTratamentoPedido)
                    .HasColumnName("Hora_Inicio_Tratamento_Pedido")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.HoraTerminoTratamentoPedido)
                    .HasColumnName("Hora_Termino_Tratamento_Pedido")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Idsupervisorexterno)
                    .HasColumnName("idsupervisorexterno")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idsupervisorinterno)
                    .HasColumnName("idsupervisorinterno")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idvendedorexterno)
                    .HasColumnName("idvendedorexterno")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Idvendedorinterno)
                    .HasColumnName("idvendedorinterno")
                    .HasColumnType("int(11)");

                entity.Property(e => e.InstalacaoBairro)
                    .HasColumnName("Instalacao_Bairro")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoCdoeCdoi)
                    .HasColumnName("Instalacao_CDOE_CDOI")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoCep)
                    .HasColumnName("Instalacao_CEP")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.InstalacaoCidade)
                    .HasColumnName("Instalacao_Cidade")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoComplemento1)
                    .HasColumnName("Instalacao_Complemento_1")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoComplemento1Tipo)
                    .HasColumnName("Instalacao_Complemento_1_tipo")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoComplemento2)
                    .HasColumnName("Instalacao_Complemento_2")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoComplemento2Tipo)
                    .HasColumnName("Instalacao_Complemento_2_tipo")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoComplemento3)
                    .HasColumnName("Instalacao_Complemento_3")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoComplemento3Tipo)
                    .HasColumnName("Instalacao_Complemento_3_tipo")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoEstado)
                    .HasColumnName("Instalacao_Estado")
                    .HasColumnType("varchar(2)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoLogradouro)
                    .HasColumnName("Instalacao_Logradouro")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoNumero)
                    .HasColumnName("Instalacao_Numero")
                    .HasColumnType("varchar(6)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.InstalacaoReferencia)
                    .HasColumnName("Instalacao_Referencia")
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.MatriculaVendedor)
                    .HasColumnName("Matricula_Vendedor")
                    .HasColumnType("varchar(8)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nacionalidade)
                    .HasColumnType("varchar(80)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NomeCliente)
                    .HasColumnName("Nome_cliente")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NomeCompletoDaMae)
                    .HasColumnName("Nome_completo_da_mae")
                    .HasColumnType("varchar(150)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.NomeVendedor)
                    .HasColumnName("Nome_Vendedor")
                    .HasColumnType("varchar(40)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ObservacaoOperador)
                    .HasColumnName("Observacao_Operador")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.ObservacaoVendedor)
                    .HasColumnName("Observacao_Vendedor")
                    .HasColumnType("varchar(200)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Operador).HasColumnType("int(11)");

                entity.Property(e => e.OsBandaLargaFixo)
                    .HasColumnName("OS_Banda_larga_Fixo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PagamentoAgencia)
                    .HasColumnName("Pagamento_Agencia")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PagamentoBanco)
                    .HasColumnName("Pagamento_Banco")
                    .HasColumnType("varchar(23)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PagamentoConta)
                    .HasColumnName("Pagamento_Conta")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PagamentoContaOnline)
                    .HasColumnName("Pagamento_Conta_Online")
                    .HasColumnType("varchar(3)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PagamentoDigito)
                    .HasColumnName("Pagamento_Digito")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PagamentoFormaDePagamento)
                    .HasColumnName("Pagamento_Forma_de_Pagamento")
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PagamentoVencimento)
                    .HasColumnName("Pagamento_Vencimento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.PeriodoAgendamentoFixoBandaLarga)
                    .HasColumnName("Periodo_agendamento_Fixo_Banda_Larga")
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Reinput).HasColumnType("tinyint(4)");

                entity.Property(e => e.Rg)
                    .HasColumnName("RG")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RgDataExpedicao)
                    .HasColumnName("rg_data_expedicao")
                    .HasColumnType("datetime");

                entity.Property(e => e.RgOExpedidor)
                    .HasColumnName("rg_o_expedidor")
                    .HasColumnType("varchar(20)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StatusPrimario)
                    .HasColumnName("Status_Primario")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StatusSecundario)
                    .HasColumnName("Status_Secundario")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TipoVenda)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.TvCanaisALaCarte)
                    .HasColumnName("TV_Canais_a_la_carte")
                    .HasColumnType("varchar(7)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TvDataAgendamento)
                    .HasColumnName("TV_Data_Agendamento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TvIdBundle)
                    .HasColumnName("TV_ID_Bundle")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TvOs)
                    .HasColumnName("TV_OS")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TvPeriodoAgendamento)
                    .HasColumnName("TV_Periodo_Agendamento")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TvPlanoTv)
                    .HasColumnName("TV_Plano_TV")
                    .HasColumnType("varchar(27)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.TvPontosAdicionais)
                    .HasColumnName("TV_Pontos_adicionais")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Usuarioalteracao)
                    .HasColumnName("usuarioalteracao")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Usuariocadastro)
                    .HasColumnName("usuariocadastro")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.VendaMesAnterior)
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.IdsupervisorexternoNavigation)
                    .WithMany(p => p.Oi360IdsupervisorexternoNavigation)
                    .HasForeignKey(d => d.Idsupervisorexterno)
                    .HasConstraintName("fk_sup_ext");

                entity.HasOne(d => d.IdsupervisorinternoNavigation)
                    .WithMany(p => p.Oi360IdsupervisorinternoNavigation)
                    .HasForeignKey(d => d.Idsupervisorinterno)
                    .HasConstraintName("fk_sup_int");

                entity.HasOne(d => d.IdvendedorexternoNavigation)
                    .WithMany(p => p.Oi360IdvendedorexternoNavigation)
                    .HasForeignKey(d => d.Idvendedorexterno)
                    .HasConstraintName("fk_vend_ext");

                entity.HasOne(d => d.IdvendedorinternoNavigation)
                    .WithMany(p => p.Oi360IdvendedorinternoNavigation)
                    .HasForeignKey(d => d.Idvendedorinterno)
                    .HasConstraintName("fk_vend_int");
            });

            modelBuilder.Entity<Oi360Detalhes>(entity =>
            {
                entity.HasIndex(e => e.NumeroPedido)
                    .HasName("fk_numpedido_idx");

                entity.HasIndex(e => e.ObservacaoAuditor)
                    .HasName("fk_status_oivende_idx");

                entity.HasIndex(e => e.StatusOiVende)
                    .HasName("fk_status_oivende_idx1");

                entity.HasIndex(e => e.Supervisor)
                    .HasName("fk_supervisor_idx");

                entity.HasIndex(e => e.Vendedor)
                    .HasName("fk_vendedor_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CheckListConfirmacaoClienteOk)
                    .HasColumnName("CheckList_ConfirmacaoCliente_OK")
                    .HasColumnType("tinyint(4)")
                    .HasComment("Booleano");

                entity.Property(e => e.CheckListDocumentoClienteOk)
                    .HasColumnName("CheckList_DocumentoCliente_OK")
                    .HasColumnType("tinyint(4)")
                    .HasComment("Booleano");

                entity.Property(e => e.CheckListPropostaOk)
                    .HasColumnName("CheckList_Proposta_OK")
                    .HasColumnType("tinyint(4)")
                    .HasComment("Booleano");

                entity.Property(e => e.DataAgendamento)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.DocumentoVenda)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.HoraAgendamento)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.NotaAtendimento)
                    .HasColumnType("int(11)")
                    .HasComment("Net Promoter Score - Nota de 1 a 10");

                entity.Property(e => e.NumeroPedido)
                    .IsRequired()
                    .HasColumnType("varchar(18)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObservacaoAuditor)
                    .HasColumnName("observacaoAuditor")
                    .HasColumnType("varchar(180)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ObservacaoPendencia)
                    .HasColumnName("observacaoPendencia")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.ProtocoloOiVende)
                    .HasColumnName("Protocolo_OiVende")
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.StatusOiVende)
                    .HasColumnName("Status_OiVende")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Supervisor).HasColumnType("int(11)");

                entity.Property(e => e.Vendedor).HasColumnType("int(11)");

                entity.HasOne(d => d.NumeroPedidoNavigation)
                    .WithMany(p => p.Oi360Detalhes)
                    .HasForeignKey(d => d.NumeroPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_numpedido");

                entity.HasOne(d => d.StatusOiVendeNavigation)
                    .WithMany(p => p.Oi360Detalhes)
                    .HasForeignKey(d => d.StatusOiVende)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_status_oivende");

                entity.HasOne(d => d.SupervisorNavigation)
                    .WithMany(p => p.Oi360Detalhes)
                    .HasForeignKey(d => d.Supervisor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_supervisor_oi360");

                entity.HasOne(d => d.VendedorNavigation)
                    .WithMany(p => p.Oi360Detalhes)
                    .HasForeignKey(d => d.Vendedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vendedor_oi360");
            });

            modelBuilder.Entity<Oi360Transferencia>(entity =>
            {
                entity.HasKey(e => e.NumeroPedido)
                    .HasName("PRIMARY");

                entity.Property(e => e.NumeroPedido)
                    .HasColumnName("numero_pedido")
                    .HasColumnType("varchar(18)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Ativo)
                    .HasColumnName("ativo")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.DataMovimento)
                    .HasColumnName("dataMovimento")
                    .HasColumnType("datetime");

                entity.Property(e => e.Movimento)
                    .HasColumnName("movimento")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Observacao)
                    .HasColumnName("observacao")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Status)
                    .HasColumnType("varchar(30)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.UsuarioNome)
                    .HasColumnName("usuarioNome")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.NumeroPedidoNavigation)
                    .WithOne(p => p.Oi360Transferencia)
                    .HasForeignKey<Oi360Transferencia>(d => d.NumeroPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_num_ped");
            });

            modelBuilder.Entity<StatusOiVende>(entity =>
            {
                entity.ToTable("Status_OiVende");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DescStatus)
                    .HasColumnName("desc_status")
                    .HasColumnType("varchar(80)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Supervisor>(entity =>
            {
                entity.HasIndex(e => e.Cpf)
                    .HasName("Cpf_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdCoordenador)
                    .HasName("fk_id_coordenador_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cpf)
                    .HasColumnType("varchar(14)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IdCoordenador)
                    .HasColumnName("idCoordenador")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Interno)
                    .HasColumnName("interno")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Nome)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.IdCoordenadorNavigation)
                    .WithMany(p => p.Supervisor)
                    .HasForeignKey(d => d.IdCoordenador)
                    .HasConstraintName("fk_id_coordenador");
            });

            modelBuilder.Entity<Uf>(entity =>
            {
                entity.HasKey(e => e.Sigla)
                    .HasName("PRIMARY");

                entity.ToTable("UF");

                entity.Property(e => e.Sigla)
                    .HasColumnType("varchar(2)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Descricao)
                    .HasColumnType("varchar(255)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.IdGrupo)
                    .HasName("fk_grupo_idx");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Cpf)
                    .HasColumnType("varchar(14)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.Email)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Foto)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.IdGrupo)
                    .HasColumnName("idGrupo")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Login)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Nome)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Senha)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Sobrenome)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Telefone)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasOne(d => d.IdGrupoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdGrupo)
                    .HasConstraintName("fk_grupo");
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasIndex(e => e.Cpf)
                    .HasName("Cpf_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.IdCanalVendas)
                    .HasName("fk_canalvendas_idx");

                entity.HasIndex(e => e.IdSupervisor)
                    .HasName("fk_supervisor_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cpf)
                    .HasColumnType("varchar(14)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.Property(e => e.IdCanalVendas)
                    .HasColumnName("idCanalVendas")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdSupervisor)
                    .HasColumnName("idSupervisor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Interno)
                    .HasColumnName("interno")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Nome)
                    .HasColumnType("varchar(60)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_swedish_ci");

                entity.HasOne(d => d.IdCanalVendasNavigation)
                    .WithMany(p => p.Vendedor)
                    .HasForeignKey(d => d.IdCanalVendas)
                    .HasConstraintName("fk_canalvendas");

                entity.HasOne(d => d.IdSupervisorNavigation)
                    .WithMany(p => p.Vendedor)
                    .HasForeignKey(d => d.IdSupervisor)
                    .HasConstraintName("fk_supervisor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
