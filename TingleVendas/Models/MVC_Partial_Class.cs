using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TingleVendas.Data;

namespace TingleVendas.Models
{
    public partial class Uf
    {
        public Uf()
        {
            Municipio = new HashSet<Municipio>();
        }

        public string Sigla { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Municipio> Municipio { get; set; }
    }
    public partial class Municipio
    {
        public int IdMunicipio { get; set; }
        public string Municipio1 { get; set; }
        public string Uf { get; set; }
        public int? Ativo { get; set; }

        public virtual Uf UfNavigation { get; set; }
    }
    public partial class Coordenador
    {
        public Coordenador()
        {
            Supervisor = new HashSet<Supervisor>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "CPF obrigatório!")]
        [Remote("CoordenadorCpfExiste", "Coordenador", HttpMethod = "Post", AdditionalFields = "Id", ErrorMessage = "O CPF informado já existe!")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Nome obrigatório!")]
        public string Nome { get; set; }

        public virtual ICollection<Supervisor> Supervisor { get; set; }
    }

    public partial class Supervisor
    {
        public Supervisor()
        {
            Oi360Detalhes = new HashSet<Oi360Detalhes>();
            Vendedor = new HashSet<Vendedor>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "CPF obrigatório!")]
        [Remote("SupervisorCpfExiste", "Supervisor", HttpMethod = "Post", AdditionalFields = "Id", ErrorMessage = "O CPF informado já existe!")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Nome obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Selecione o coordenador!")]
        public int? IdCoordenador { get; set; }
        public bool Interno { get; set; }

        public virtual Coordenador IdCoordenadorNavigation { get; set; }
        public virtual ICollection<Oi360Detalhes> Oi360Detalhes { get; set; }
        public virtual ICollection<Oi360> Oi360IdsupervisorexternoNavigation { get; set; }
        public virtual ICollection<Oi360> Oi360IdsupervisorinternoNavigation { get; set; }
        public virtual ICollection<Vendedor> Vendedor { get; set; }
    }

    public partial class Vendedor
    {
        public Vendedor()
        {
            Oi360Detalhes = new HashSet<Oi360Detalhes>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "CPF obrigatório!")]
        [Remote("VendedorCpfExiste", "Vendedor", HttpMethod = "Post", AdditionalFields = "Id", ErrorMessage = "O CPF informado já existe!")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Nome obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Selecione o supervisor")]
        public int? IdSupervisor { get; set; }
        [Required(ErrorMessage = "Selecione o canal de vendas")]
        public int? IdCanalVendas { get; set; }
        public bool Interno { get; set; }

        public virtual CanalVendas IdCanalVendasNavigation { get; set; }
        public virtual Supervisor IdSupervisorNavigation { get; set; }
        public virtual ICollection<Oi360Detalhes> Oi360Detalhes { get; set; }
        public virtual ICollection<Oi360> Oi360IdvendedorexternoNavigation { get; set; }
        public virtual ICollection<Oi360> Oi360IdvendedorinternoNavigation { get; set; }
    }

    public partial class Usuario
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome obrigatório!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Sobrenome obrigatório!")]
        public string Sobrenome { get; set; }
        [Required(ErrorMessage = "E-mail obrigatório!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Telefone obrigatório!")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Login obrigatório!")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Senha obrigatório!")]
        public string Senha { get; set; }
        public string Foto { get; set; }
        [Required(ErrorMessage = "Selecione o grupo!")]
        public int? IdGrupo { get; set; }
        [Required(ErrorMessage = "CPF obrigatório!")]
        public string Cpf { get; set; }

        public virtual Grupo IdGrupoNavigation { get; set; }
    }

    public partial class CanalVendas
    {
        public CanalVendas()
        {
            Vendedor = new HashSet<Vendedor>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Canal de vendas obrigatório!")]
        public string Descricao { get; set; }

        public virtual ICollection<Vendedor> Vendedor { get; set; }
    }

    public partial class Oi360
    {
        public Oi360()
        {
            Oi360Detalhes = new HashSet<Oi360Detalhes>();
        }

        [Required(ErrorMessage = "Protocolo do sistema obrigatório!")]
        public string NumeroDoPedido { get; set; }
        [Required(ErrorMessage = "A data do pedido é obrigatória!")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataEmQueOPedidoFoiRealizado { get; set; }
        public string HoraEmQueOPedidoFoiRealizado { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataEmQueOPedidoFoiTratado { get; set; }
        //Sistema de entrada
        public string HoraEmQueOPedidoFoiTratado { get; set; }
        [Required(ErrorMessage = "O nome do cliente é obrigatório!")]
        public string NomeCliente { get; set; }
        [Required(ErrorMessage = "Gênero obrigatório!")]    
        public string Genero { get; set; }
        public DateTime? DataDeNascimento { get; set; }
        [Required(ErrorMessage = "O CPF é obrigatório!")]
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string RgOExpedidor { get; set; }
        public DateTime? RgDataExpedicao { get; set; }
        public string NomeCompletoDaMae { get; set; }
        public string Nacionalidade { get; set; }
        [Required(ErrorMessage = "Contato principal obrigatório!")]
        public string ContatoPrincipal { get; set; }
        public string ContatoPrincipalWhatsapp { get; set; }
        [Required(ErrorMessage = "Contato secundário obrigatório!")]
        public string ContatoSecundario { get; set; }
        public string ContatoSecundarioWhatsapp { get; set; }
        public string Contato1Nome { get; set; }
        public string Contato1Numero { get; set; }
        public string Contato2Nome { get; set; }
        public string Contato2Numero { get; set; }
        public string Contato3Nome { get; set; }
        public string Contato3Numero { get; set; }
        [Required(ErrorMessage = "E-mail obrigatório!")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string EMail { get; set; }
        public string MatriculaVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string FixoOi { get; set; }
        public string FixoPortabilidade { get; set; }
        public string FixoOperadora { get; set; }
        public string FixoNumero { get; set; }

        [Required(ErrorMessage = "Tipo de plano obrigatório!")]
        //Tipo de plano
        public string BandaLargaTecnologia { get; set; }
        //Velocidade fibra
        [Required(ErrorMessage = "Velocidade obrigatória!")]
        public string BandaLargaVelocidade { get; set; }
        public DateTime? BandaLargaDataAgendamento { get; set; }
        public string PeriodoAgendamentoFixoBandaLarga { get; set; }
        public int? OsBandaLargaFixo { get; set; }
        //Tipo de TV
        public string TvPlanoTv { get; set; }
        public int? TvPontosAdicionais { get; set; }
        public string TvCanaisALaCarte { get; set; }
        public int? TvOs { get; set; }
        public int? TvDataAgendamento { get; set; }
        public int? TvPeriodoAgendamento { get; set; }
        public int? TvIdBundle { get; set; }
        public string ComboContratado { get; set; }

        [Required(ErrorMessage = "Forma de pagamento obrigatória!")]
        //[Remote("ValidaFormaPagamento", "Oi360", HttpMethod = "Post", AdditionalFields = "PagamentoFormaDePagamento,PagamentoBanco,PagamentoAgencia,PagamentoConta", ErrorMessage = "Campo obrigatório!")]
        public string PagamentoFormaDePagamento { get; set; }
        [Required(ErrorMessage = "Data de vencimento obrigatória!")]
        public int? PagamentoVencimento { get; set; }
        public string PagamentoContaOnline { get; set; }
        //[Remote("ValidaFormaPagamento", "Oi360", HttpMethod = "Post", AdditionalFields = "PagamentoFormaDePagamento,PagamentoBanco,PagamentoAgencia,PagamentoConta", ErrorMessage = "Campo obrigatório!")]

        [RequiredIf(nameof(PagamentoFormaDePagamento), "Débito", ErrorMessage = "Campo obrigatório")]
        //[Remote("ValidaFormaPagamentoBanco", "Oi360", HttpMethod = "Post", AdditionalFields = "PagamentoFormaDePagamento,PagamentoAgencia,PagamentoConta", ErrorMessage = "Campo obrigatório!")]
        public string PagamentoBanco { get; set; }
        [RequiredIf(nameof(PagamentoFormaDePagamento), "Débito", ErrorMessage = "Campo obrigatório")]
        public string PagamentoAgencia { get; set; }
        [RequiredIf(nameof(PagamentoFormaDePagamento), "Débito", ErrorMessage = "Campo obrigatório")]
        public string PagamentoConta { get; set; }
        public int? PagamentoDigito { get; set; }
        public string StatusPrimario { get; set; }
        public string StatusSecundario { get; set; }
        public string ObservacaoVendedor { get; set; }
        public string ObservacaoOperador { get; set; }
        public int? Operador { get; set; }
        public DateTime? DataInicioTratamentoPedido { get; set; }
        public string HoraInicioTratamentoPedido { get; set; }
        public DateTime? DataTerminoTratamentoPedido { get; set; }
        public string HoraTerminoTratamentoPedido { get; set; }
        //Inicio Campos editaveis
        //[Required(ErrorMessage = "O Cep é obrigatório!")]
        public string InstalacaoCep { get; set; }
        //[Required(ErrorMessage = "O Logradouro é obrigatório!")]
        public string InstalacaoLogradouro { get; set; }
        //[Required(ErrorMessage = "O Número é obrigatório!")]
        public string InstalacaoNumero { get; set; }
        [Required(ErrorMessage = "O Bairro é obrigatório!")]
        [Remote("CpfBairroExiste", "Oi360", HttpMethod = "Post", AdditionalFields = "Cpf,NumeroDoPedido", ErrorMessage = "Já existe um cadastro para este CPF neste Bairro!")]
        public string InstalacaoBairro { get; set; }
        //Fim Campos editaveis
        [Required(ErrorMessage = "Cidade obrigatório!")]
        public string InstalacaoCidade { get; set; }
        [Required(ErrorMessage = "Estado obrigatório!")]
        public string InstalacaoEstado { get; set; }
        [Required(ErrorMessage = "Referência obrigatória!")]
        public string InstalacaoReferencia { get; set; }
        [Required(ErrorMessage = "Cdoe/Cdoi é obrigatório!")]
        public string InstalacaoCdoeCdoi { get; set; }
        //Inicio Campos editaveis
        public string InstalacaoComplemento1Tipo { get; set; }
        public string InstalacaoComplemento1 { get; set; }
        //Fim Campos editaveis
        public string InstalacaoComplemento2Tipo { get; set; }
        public string InstalacaoComplemento2 { get; set; }
        public string InstalacaoComplemento3Tipo { get; set; }
        public string InstalacaoComplemento3 { get; set; }
        public string CobrancaCep { get; set; }
        public string CobrancaLogradouro { get; set; }
        public string CobrancaNumero { get; set; }
        public string CobrancaBairro { get; set; }
        public string CobrancaCidade { get; set; }
        public string CobrancaEstado { get; set; }
        public string CobrancaComplemento1Tipo { get; set; }
        public string CobrancaComplemento1 { get; set; }
        public string CobrancaComplemento2Tipo { get; set; }
        public string CobrancaComplemento2 { get; set; }
        public string CobrancaComplemento3Tipo { get; set; }
        public string CobrancaComplemento3 { get; set; }

        //[Required(ErrorMessage = "Tipo de venda obrigatório!")]
        public string TipoVenda { get; set; }
        public string VendaMesAnterior { get; set; }
        public bool Reinput { get; set; }
        public DateTime? Datacadastro { get; set; }
        public string Usuariocadastro { get; set; }
        public DateTime? Dataalteracao { get; set; }
        public string Usuarioalteracao { get; set; }
        public int? Idsupervisorinterno { get; set; }
        public int? Idvendedorinterno { get; set; }
        public int? Idsupervisorexterno { get; set; }
        public int? Idvendedorexterno { get; set; }

        public virtual Supervisor IdsupervisorexternoNavigation { get; set; }
        public virtual Supervisor IdsupervisorinternoNavigation { get; set; }
        public virtual Vendedor IdvendedorexternoNavigation { get; set; }
        public virtual Vendedor IdvendedorinternoNavigation { get; set; }
        public virtual Oi360Transferencia Oi360Transferencia { get; set; }
        public virtual ICollection<Oi360Detalhes> Oi360Detalhes { get; set; }
    }

    public partial class Oi360Detalhes
    {
        public int Id { get; set; }
        public string NumeroPedido { get; set; }
        public int? Supervisor { get; set; }
        public int? Vendedor { get; set; }
        public string ObservacaoAuditor { get; set; }
        public int StatusOiVende { get; set; }
        public string ProtocoloOiVende { get; set; }
        public string DocumentoVenda { get; set; }
        public bool CheckListDocumentoClienteOk { get; set; }
        public bool CheckListPropostaOk { get; set; }
        public bool CheckListConfirmacaoClienteOk { get; set; }
        public int? NotaAtendimento { get; set; }
        public string DataAgendamento { get; set; }
        public string HoraAgendamento { get; set; }
        public string ObservacaoPendencia { get; set; }

        public virtual Oi360 NumeroPedidoNavigation { get; set; }
        public virtual StatusOiVende StatusOiVendeNavigation { get; set; }
        public virtual Supervisor SupervisorNavigation { get; set; }
        public virtual Vendedor VendedorNavigation { get; set; }

    }

    public partial class StatusOiVende
    {
        public StatusOiVende()
        {
            Oi360Detalhes = new HashSet<Oi360Detalhes>();
        }

        public int Id { get; set; }
        public string DescStatus { get; set; }

        public virtual ICollection<Oi360Detalhes> Oi360Detalhes { get; set; }
    }

    public partial class Grupo
    {
        public Grupo()
        {
            GrupoMenu = new HashSet<GrupoMenu>();
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<GrupoMenu> GrupoMenu { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }

    public partial class GrupoMenu
    {
        public int IdGrupo { get; set; }
        public int Idmenu { get; set; }

        public virtual Grupo IdGrupoNavigation { get; set; }
        public virtual Menu IdmenuNavigation { get; set; }
    }

    public partial class Menu
    {
        public Menu()
        {
            GrupoMenu = new HashSet<GrupoMenu>();
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }

        public virtual ICollection<GrupoMenu> GrupoMenu { get; set; }
    }

    public partial class Bov
    {
        public int Id { get; set; }
        public int? CodigoSapPdv { get; set; }
        public string GrupoUnidade { get; set; }
        public string NumeroPedido { get; set; }
        public string Produto { get; set; }
        public string Tipo { get; set; }
        public DateTime? DataPedido { get; set; }
        public string BovStatus { get; set; }
        public DateTime? DataStatus { get; set; }
        public string MotivoRetirada { get; set; }
        public string NumIdentidade { get; set; }
        public string NomeCliente { get; set; }
        public string SegmentoMercado { get; set; }
        public string TelefoneContato { get; set; }
        public string MetodoPagamento { get; set; }
        public int? DiaVencimentoFatura { get; set; }
        public DateTime? DataCorte { get; set; }
        public string TipoLogradouroInstalacao { get; set; }
        public string NomeLogradouroInstalacao { get; set; }
        public string NumeroInstalacao { get; set; }
        public string TipoCompInstalacao { get; set; }
        public string NumComp1Instalacao { get; set; }
        public string NumComp2Instalacao { get; set; }
        public string NumComp3Instalacao { get; set; }
        public string BairroInstalacao { get; set; }
        public string MunicipioInstalacao { get; set; }
        public string EstadoInstalacao { get; set; }
        public string CepInstalacao { get; set; }
        public int? NumLocalidade { get; set; }
        public string CodigoEstacao { get; set; }
        public string IdUnicoAcesso { get; set; }
        public string IdAcessoAssociado { get; set; }
        public string NumeroContrato { get; set; }
        public string AcessoGpon { get; set; }
        public int? DddFixo { get; set; }
        public int? NumeroFixo { get; set; }
        public string ClassePlano { get; set; }
        public string Velocidade { get; set; }
        public string QtdPontosAdicionaisTv { get; set; }
        public string Tecnologia { get; set; }
        public string LinhaProduto { get; set; }
        public string Plano { get; set; }
        public string PlanoAnterior { get; set; }
        public string NomeCampanha { get; set; }
        public string NomeOferta { get; set; }
        public string NomeCampanhaAnterior { get; set; }
        public string NomeOfertaAnterior { get; set; }
        public string NomeCampanhaOriginal { get; set; }
        public string NomeOfertaOriginal { get; set; }
        public string CodigoSap { get; set; }
        public string PontoVenda { get; set; }
        public string TipoCanal { get; set; }
        public string LoginVendedor { get; set; }
        public string PedidoCriadoPor { get; set; }
        public string AdicaoPonto { get; set; }
        public string NumeroAtivoPedido { get; set; }
        public string NumeroDocumento { get; set; }
        public string TipoPedido { get; set; }
        public int? ClasseProduto { get; set; }
        public string QtdTotalPontos { get; set; }
        public string InteressePortabilidade { get; set; }
        public string TipoPromocao { get; set; }
        public int? IdBundle { get; set; }
        public DateTime? DtAbertura { get; set; }
        public DateTime? CriadoEm { get; set; }
        public DateTime? ModificadoEm { get; set; }
        public string IndCombo { get; set; }
        public string PlanoBundle { get; set; }
        public string TipoPosseCombo { get; set; }
        public string CdCampMovelTit { get; set; }
        public string CampMovelTit { get; set; }
        public string CdOftMovelTit { get; set; }
        public string SitBundle { get; set; }
        public string TipoRede { get; set; }
        public int? DescricaoCanalBov { get; set; }
        public string DtFechamento { get; set; }
        public string Evento { get; set; }
        public string BundleTipoEventoVenda { get; set; }
        public string BundleSitFixo { get; set; }
        public string BundleSitVlx { get; set; }
        public string BundleSitTv { get; set; }
        public string BundleSitMovelTit { get; set; }
        public string FlgVendaValida { get; set; }
        public string BundleProdutoNovo { get; set; }
        public string UnidadeNegocio { get; set; }
        public int? TpRetirada { get; set; }
        public string BundleMaMovelTit { get; set; }
        public string BundleNumeroDocumento { get; set; }
        public string BundleContratoTv { get; set; }
        public string BundleNumeroDocumentoTv { get; set; }
        public string FlgMigCobreFixo { get; set; }
        public string FlgMigCobreVelox { get; set; }
        public string FlgMigTv { get; set; }
        public string LoginOit { get; set; }
        public string MotivoSituacaoPedido { get; set; }
    }

    public partial class Click
    {
        public DateTime? DataCarga { get; set; }
        public int Id { get; set; }
        public string Atividade { get; set; }
        public string GrupoUnidade { get; set; }
        public DateTime? Inicioagendamento { get; set; }
        public DateTime? Fimagendamento { get; set; }
        public string Filial { get; set; }
        public string Ddd { get; set; }
        public string FilialIi { get; set; }
        public string Gc { get; set; }
        public string Gv { get; set; }
        public string GrupoCanal { get; set; }
        public int? CodSap { get; set; }
        public string NomePdv { get; set; }
        public string NumOs { get; set; }
        public string IndVendaConjunta { get; set; }
        public string DataFechamento { get; set; }
        public string DataFimPriAgendamento { get; set; }
        public string DataFimUltAgendamento { get; set; }
        public string QtdReagendamentoClick { get; set; }
        public string Agendado { get; set; }
        public string Codencerramento { get; set; }
        public string Estado { get; set; }
        public string Safra { get; set; }
        public string NomeMunicipio { get; set; }
        public string Gram { get; set; }
        public string Gra { get; set; }
        public string Matriculatecnico { get; set; }
        public string Tecnico { get; set; }
        public string MatriculaVendedor { get; set; }
        public string Nrba { get; set; }
        public string GcCarteira { get; set; }
        public string Contato1 { get; set; }
        public string Contato2 { get; set; }
        public string Contato3 { get; set; }
        public string Contatoefetivo { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCliente { get; set; }
        public string Coordenador { get; set; }
        public string TelCoord { get; set; }
        public string Prontoparaexecucao { get; set; }
        public string TipoLogr { get; set; }
        public string NomeLogr { get; set; }
        public string NumPorta { get; set; }
        public string TipoCompl { get; set; }
        public string Compl { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string NumLoc { get; set; }
        public string StatusFechada { get; set; }
        public string DescStatus { get; set; }
    }

    public partial class Oi360Transferencia
    {
        [Required(ErrorMessage = "Número do pedido obrigatório!")]
        public string NumeroPedido { get; set; }
        [Required(ErrorMessage = "O Status é obrigatório!")]
        public string Status { get; set; }
        [Required(ErrorMessage = "Digite uma observação!")]
        public string Observacao { get; set; }
        [Required(ErrorMessage = "Usuário obrigatório!")]
        public string UsuarioNome { get; set; }
        [Required(ErrorMessage = "Data obrigatória!")]
        public DateTime? DataMovimento { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Movimento { get; set; }
        public int? Ativo { get; set; }

        public virtual Oi360 NumeroPedidoNavigation { get; set; }
    }

    public partial class LogImportacao
    {
        public int Id { get; set; }
        public string Planilha { get; set; }
        public string DataHora { get; set; }
        public string Chave1 { get; set; }
        public string Chave2 { get; set; }
        public string Chave3 { get; set; }
        public int? Linha { get; set; }
        public string Erro { get; set; }
    }

    public partial class LogImportacao2
    {
        public int Id { get; set; }
        public string Planilha { get; set; }
        public DateTime DataHora { get; set; }
        public string Chave1 { get; set; }
        public string Chave2 { get; set; }
        public string Chave3 { get; set; }
        public int? Linha { get; set; }
        public string Erro { get; set; }
    }

}
