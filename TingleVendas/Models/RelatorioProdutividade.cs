using System;
namespace TingleVendas.Models
{
    public class RelatorioProdutividade
    {
        public RelatorioProdutividade()
        {
        }

        public string NomeSupervisorInterno { get; set; }
        public string NomeSupervisorExterno { get; set; }
        public string NomeVendedorInterno { get; set; }
        public string NomeVendedorExterno { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }
        public string BovStatus { get; set; }
        public string TipoPedido { get; set; }
        public int Total { get; set; }

    }
}
