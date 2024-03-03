using Biciclotti.Models;
using Biciclotti.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Biciclotti.Data
{
    public sealed class DbEntities : DbContext
    {
        /// <summary>
        /// appsettings caricato
        /// </summary>
        IConfiguration config;

        public DbEntities() { }
        public DbEntities(DbContextOptions<DbEntities> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            config ??= new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Definisco il vincolo di univocità tra Marca e Modello
            modelBuilder.Entity<Bicycle>()
                .HasIndex(b => new { b.Marca, b.Modello })
                .IsUnique();

            // Definisci la relazione tra Bicycle e Stock
            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Bicycle)
                .WithMany()
                .HasForeignKey(s => s.BicycleId);
            
            // Vincolo di unicità sulla coppia bicicletta-taglia
            modelBuilder.Entity<Stock>()
                .HasIndex(s => new { s.BicycleId, s.Taglia })
                .IsUnique();


            modelBuilder.Entity<OrderRow>()
                .HasOne(o => o.Bicycle)
                .WithMany()
                .HasForeignKey(o => o.BicycleId);


            modelBuilder.Entity<OrderRow>()
                .HasOne(o => o.order)
                .WithMany()
                .HasForeignKey(o => o.CodiceOrdine);

            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.IdCliente);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
                
        }

        #region Models

        public DbSet<Bicycle> Bicycles { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Stock> Stocks { get; set; }     
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<User> Users { get; set; }

        #endregion
    }
}