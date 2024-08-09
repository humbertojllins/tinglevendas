//using System;
//using System.Collections.Generic;

//namespace TingleVendas.Models
//{
//    public partial class Oi360Detalhes
//    {
//        public int Id { get; set; }
//        public string NumeroPedido { get; set; }
//        public int Supervisor { get; set; }
//        public int Vendedor { get; set; }
//        public string ObservacaoAuditor { get; set; }
//        public int StatusOiVende { get; set; }
//        public string ProtocoloOiVende { get; set; }
//        public string DocumentoVenda { get; set; }
//        public sbyte CheckListDocumentoClienteOk { get; set; }
//        public sbyte CheckListPropostaOk { get; set; }
//        public sbyte CheckListConfirmacaoClienteOk { get; set; }
//        public int? NotaAtendimento { get; set; }
//        public string DataAgendamento { get; set; }
//        public string HoraAgendamento { get; set; }
//        public string ObservacaoPendencia { get; set; }

//        public virtual Oi360 NumeroPedidoNavigation { get; set; }
//        public virtual StatusOiVende StatusOiVendeNavigation { get; set; }
//        public virtual Supervisor SupervisorNavigation { get; set; }
//        public virtual Vendedor VendedorNavigation { get; set; }
//    }
//}
