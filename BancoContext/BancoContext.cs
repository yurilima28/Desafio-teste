using Intelectah.Models;
using Microsoft.EntityFrameworkCore;

namespace Intelectah.Dapper
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }
        public DbSet<FabricantesModel> Fabricantes { get; set; }
        public DbSet<VeiculosModel> Veiculos { get; set; }
        public DbSet<ConcessionariasModel> Concessionarias { get; set; }
        public DbSet<VendasModel> Vendas { get; set; }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<UsuariosModel> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VeiculosModel>()
        .HasOne(v => v.Fabricante)
        .WithMany(f => f.Veiculos)
        .HasForeignKey(v => v.FabricanteID)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VendasModel>()
                .HasOne(v => v.Veiculo)
                .WithMany(v => v.Vendas)
                .HasForeignKey(v => v.VeiculoID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VendasModel>()
                .HasOne(v => v.Fabricante)
                .WithMany()
                .HasForeignKey(v => v.FabricanteID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VendasModel>()
                .HasOne(v => v.Usuario)
                .WithMany()
                .HasForeignKey(v => v.UsuarioID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<VendasModel>()
                .HasOne(v => v.Concessionaria)
                .WithMany()
                .HasForeignKey(v => v.ConcessionariaID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VendasModel>()
                .HasOne(v => v.Cliente)
                .WithMany()
                .HasForeignKey(v => v.ClienteID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
