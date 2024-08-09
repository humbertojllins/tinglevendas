//using System;
//using System.Collections.Generic;

//namespace TingleVendas.Models
//{
//    public partial class Oi360
//    {
//        public Oi360()
//        {
//            Oi360Detalhes = new HashSet<Oi360Detalhes>();
//        }

//        public string NumeroDoPedido { get; set; }
//        public DateTime? DataEmQueOPedidoFoiRealizado { get; set; }
//        public string HoraEmQueOPedidoFoiRealizado { get; set; }
//        public DateTime? DataEmQueOPedidoFoiTratado { get; set; }
//        public string HoraEmQueOPedidoFoiTratado { get; set; }
//        public string NomeCliente { get; set; }
//        public string Genero { get; set; }
//        public DateTime? DataDeNascimento { get; set; }
//        public string Cpf { get; set; }
//        public string Rg { get; set; }
//        public string RgOExpedidor { get; set; }
//        public DateTime? RgDataExpedicao { get; set; }
//        public string NomeCompletoDaMae { get; set; }
//        public string Nacionalidade { get; set; }
//        public string ContatoPrincipal { get; set; }
//        public string ContatoPrincipalWhatsapp { get; set; }
//        public string ContatoSecundario { get; set; }
//        public string ContatoSecundarioWhatsapp { get; set; }
//        public string Contato1Nome { get; set; }
//        public string Contato1Numero { get; set; }
//        public string Contato2Nome { get; set; }
//        public string Contato2Numero { get; set; }
//        public string Contato3Nome { get; set; }
//        public string Contato3Numero { get; set; }
//        public string EMail { get; set; }
//        public string MatriculaVendedor { get; set; }
//        public string NomeVendedor { get; set; }
//        public string FixoOi { get; set; }
//        public string FixoPortabilidade { get; set; }
//        public string FixoOperadora { get; set; }
//        public string FixoNumero { get; set; }
//        public string BandaLargaTecnologia { get; set; }
//        public string BandaLargaVelocidade { get; set; }
//        public DateTime? BandaLargaDataAgendamento { get; set; }
//        public string PeriodoAgendamentoFixoBandaLarga { get; set; }
//        public int? OsBandaLargaFixo { get; set; }
//        public string TvPlanoTv { get; set; }
//        public int? TvPontosAdicionais { get; set; }
//        public string TvCanaisALaCarte { get; set; }
//        public int? TvOs { get; set; }
//        public int? TvDataAgendamento { get; set; }
//        public int? TvPeriodoAgendamento { get; set; }
//        public int? TvIdBundle { get; set; }
//        public string ComboContratado { get; set; }
//        public string PagamentoFormaDePagamento { get; set; }
//        public int? PagamentoVencimento { get; set; }
//        public string PagamentoContaOnline { get; set; }
//        public string PagamentoBanco { get; set; }
//        public string PagamentoAgencia { get; set; }
//        public string PagamentoConta { get; set; }
//        public int? PagamentoDigito { get; set; }
//        public string StatusPrimario { get; set; }
//        public string StatusSecundario { get; set; }
//        public string ObservacaoVendedor { get; set; }
//        public string ObservacaoOperador { get; set; }
//        public int? Operador { get; set; }
//        public DateTime? DataInicioTratamentoPedido { get; set; }
//        public string HoraInicioTratamentoPedido { get; set; }
//        public DateTime? DataTerminoTratamentoPedido { get; set; }
//        public string HoraTerminoTratamentoPedido { get; set; }
//        public string InstalacaoCep { get; set; }
//        public string InstalacaoLogradouro { get; set; }
//        public string InstalacaoNumero { get; set; }
//        public string InstalacaoBairro { get; set; }
//        public string InstalacaoCidade { get; set; }
//        public string InstalacaoEstado { get; set; }
//        public string InstalacaoReferencia { get; set; }
//        public string InstalacaoCdoeCdoi { get; set; }
//        public string InstalacaoComplemento1Tipo { get; set; }
//        public string InstalacaoComplemento1 { get; set; }
//        public string InstalacaoComplemento2Tipo { get; set; }
//        public string InstalacaoComplemento2 { get; set; }
//        public string InstalacaoComplemento3Tipo { get; set; }
//        public string InstalacaoComplemento3 { get; set; }
//        public string CobrancaCep { get; set; }
//        public string CobrancaLogradouro { get; set; }
//        public string CobrancaNumero { get; set; }
//        public string CobrancaBairro { get; set; }
//        public string CobrancaCidade { get; set; }
//        public string CobrancaEstado { get; set; }
//        public string CobrancaComplemento1Tipo { get; set; }
//        public string CobrancaComplemento1 { get; set; }
//        public string CobrancaComplemento2Tipo { get; set; }
//        public string CobrancaComplemento2 { get; set; }
//        public string CobrancaComplemento3Tipo { get; set; }
//        public string CobrancaComplemento3 { get; set; }
//        public string TipoVenda { get; set; }
//        public string VendaMesAnterior { get; set; }
//        public sbyte? Reinput { get; set; }
//        public DateTime? Datacadastro { get; set; }
//        public string Usuariocadastro { get; set; }
//        public DateTime? Dataalteracao { get; set; }
//        public string Usuarioalteracao { get; set; }
//        public int? Idsupervisorinterno { get; set; }
//        public int? Idvendedorinterno { get; set; }
//        public int? Idsupervisorexterno { get; set; }
//        public int? Idvendedorexterno { get; set; }

//        public virtual Supervisor IdsupervisorexternoNavigation { get; set; }
//        public virtual Supervisor IdsupervisorinternoNavigation { get; set; }
//        public virtual Vendedor IdvendedorexternoNavigation { get; set; }
//        public virtual Vendedor IdvendedorinternoNavigation { get; set; }
//        public virtual Oi360Transferencia Oi360Transferencia { get; set; }
//        public virtual ICollection<Oi360Detalhes> Oi360Detalhes { get; set; }
//    }
//}
