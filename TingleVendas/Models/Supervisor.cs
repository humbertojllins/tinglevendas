//using System;
//using System.Collections.Generic;

//namespace TingleVendas.Models
//{
//    public partial class Supervisor
//    {
//        public Supervisor()
//        {
//            Oi360Detalhes = new HashSet<Oi360Detalhes>();
//            Oi360IdsupervisorexternoNavigation = new HashSet<Oi360>();
//            Oi360IdsupervisorinternoNavigation = new HashSet<Oi360>();
//            Vendedor = new HashSet<Vendedor>();
//        }

//        public int Id { get; set; }
//        public string Cpf { get; set; }
//        public string Nome { get; set; }
//        public int? IdCoordenador { get; set; }
//        public int? Interno { get; set; }

//        public virtual Coordenador IdCoordenadorNavigation { get; set; }
//        public virtual ICollection<Oi360Detalhes> Oi360Detalhes { get; set; }
//        public virtual ICollection<Oi360> Oi360IdsupervisorexternoNavigation { get; set; }
//        public virtual ICollection<Oi360> Oi360IdsupervisorinternoNavigation { get; set; }
//        public virtual ICollection<Vendedor> Vendedor { get; set; }
//    }
//}
