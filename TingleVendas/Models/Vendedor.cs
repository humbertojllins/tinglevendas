//using System;
//using System.Collections.Generic;

//namespace TingleVendas.Models
//{
//    public partial class Vendedor
//    {
//        public Vendedor()
//        {
//            Oi360Detalhes = new HashSet<Oi360Detalhes>();
//            Oi360IdvendedorexternoNavigation = new HashSet<Oi360>();
//            Oi360IdvendedorinternoNavigation = new HashSet<Oi360>();
//        }

//        public int Id { get; set; }
//        public string Cpf { get; set; }
//        public string Nome { get; set; }
//        public int? IdSupervisor { get; set; }
//        public int? IdCanalVendas { get; set; }
//        public int? Interno { get; set; }

//        public virtual CanalVendas IdCanalVendasNavigation { get; set; }
//        public virtual Supervisor IdSupervisorNavigation { get; set; }
//        public virtual ICollection<Oi360Detalhes> Oi360Detalhes { get; set; }
//        public virtual ICollection<Oi360> Oi360IdvendedorexternoNavigation { get; set; }
//        public virtual ICollection<Oi360> Oi360IdvendedorinternoNavigation { get; set; }
//    }
//}
