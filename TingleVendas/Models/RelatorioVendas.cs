using System;
namespace TingleVendas.Models
{
    public class RelatorioVendas
    {
        public RelatorioVendas()
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
        public string NomeSupervisor { get; set; }
        public string NomeVendedor { get; set; }
        public string Reinput { get; set; }
    }

    public class RelatorioVendasResumo
    {
        public RelatorioVendasResumo()
        {
        }

        public int Ano { get; set; }
        public int Mes { get; set; }
        public int qtdConcluido { get; set; }
        public int qtdPendente { get; set; }
        public int qtdCancelado { get; set; }
        public int qtdErro { get; set; }
    }
}


