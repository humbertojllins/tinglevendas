using System;
namespace TingleVendas.Models
{
    public class RelatorioPendencias
    {
        public RelatorioPendencias()
        {
        }

        public string NumPedido { get; set; }
        public string Cpf { get; set; }
        public string NomeCliente { get; set; }
        public string BovStatus { get; set; }
        public string TipoPedido { get; set; }
        public string Bairro { get; set; }
        public int QtdPedidosCancelados { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public DateTime DataPedido { get; set; }
        public string NomeSupervisorInterno { get; set; }
        public string NomeSupervisorExterno { get; set; }
        public string NomeVendedorInterno { get; set; }
        public string NomeVendedorExterno { get; set; }
        public string Status_Fechada { get; set; }
        public string Desc_Status { get; set; }
        public int qtd_atribuicoes { get; set; }
        public int agendamentofuturo { get; set; }
        public DateTime ultimoAgendamento { get; set; }
        public string status_pendencia { get; set; }
        public string observacaoPendencia { get; set; }
        
    }
}
