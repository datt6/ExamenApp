using AplicacionWebNomina.Models;
using System.Data.Entity;

namespace AplicacionWebNomina.Data
{
    public class ApplicationDContext : DbContext
    {
        public ApplicationDContext() : base("DefaultConnection")
        {
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrdenCompra> Orders { get; set; }
        public DbSet<DetalleOrdenCompra> DetalleOrdenCompra { get; set; } // Agregado DbSet para DetalleOrdenCompra

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configuración para Stock
            modelBuilder.Entity<Stock>()
                .HasKey(s => new { s.StoreId, s.ProductId });

            // Configuración para OrdenCompra
            modelBuilder.Entity<OrdenCompra>()
                .HasKey(o => o.OrdenCompraId);

            modelBuilder.Entity<OrdenCompra>()
                .HasRequired(o => o.Cliente)
                .WithMany() // o .WithMany(c => c.Ordenes) si es una colección inversa
                .HasForeignKey(o => o.ClienteId);

            modelBuilder.Entity<OrdenCompra>()
                .HasRequired(o => o.Tienda)
                .WithMany() // o .WithMany(s => s.Ordenes) si es una colección inversa
                .HasForeignKey(o => o.TiendaId);

            modelBuilder.Entity<OrdenCompra>()
                .HasRequired(o => o.Empleado)
                .WithMany() // o .WithMany(e => e.Ordenes) si es una colección inversa
                .HasForeignKey(o => o.EmpleadoId);

            // Configuración para DetalleOrdenCompra
            modelBuilder.Entity<DetalleOrdenCompra>()
                .HasKey(d => d.DetalleOrdenCompraId);

            modelBuilder.Entity<DetalleOrdenCompra>()
                .HasRequired(d => d.OrdenCompra)
                .WithMany(o => o.DetalleOrden) // Especifica la relación inversa
                .HasForeignKey(d => d.OrdenCompraId);

            modelBuilder.Entity<DetalleOrdenCompra>()
                .HasRequired(d => d.Producto)
                .WithMany() // o .WithMany(p => p.DetallesOrden) si es una colección inversa
                .HasForeignKey(d => d.CodigoProducto);

            base.OnModelCreating(modelBuilder);
        }
    }
}
