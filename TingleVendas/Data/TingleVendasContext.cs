using System;
using Microsoft.EntityFrameworkCore;
using TingleVendas.Models;

namespace TingleVendas.Data
{
    public class TingleVendasContext : DbContext
    {
        public TingleVendasContext(DbContextOptions<TingleVendasContext> options)
             : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }

        public DbSet<Usuario> Usuario { get; set; }
    }
}
