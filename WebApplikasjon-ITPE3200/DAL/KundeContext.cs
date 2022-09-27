using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplikasjon_ITPE3200.DAL;

namespace WebApplikasjon_ITPE3200.Models
{
    [ExcludeFromCodeCoverage]
    public class KundeContext : DbContext
    {

        public KundeContext(DbContextOptions<KundeContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Kunde> Kunder { get; set; }
        public virtual DbSet<Turer> Turer { get; set; }
        public virtual DbSet<Stasjon> Stasjoner { get; set; }
        public virtual DbSet<Bestilling> Bestillinger { get; set; }
        public virtual DbSet<Brukere> Brukere { get; set; }
        public DbSet<Kreditt> Kreditt { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    } 
}
