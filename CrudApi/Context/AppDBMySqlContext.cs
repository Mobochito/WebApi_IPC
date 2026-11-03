using CrudApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Context
{
    public class AppDbMySqlContext : DbContext, IApplicationDbContext
    {
        public AppDbMySqlContext(DbContextOptions<AppDbMySqlContext> options) : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Perfil>(tb =>
            {
                tb.HasKey(col => col.IdPerfil);

                // CORRECCIÓN: Se elimina .UseIdentityColumn(). 
                // Con EF Core y Pomelo MySQL, ValueGeneratedOnAdd() mapea automáticamente a AUTO_INCREMENT.
                tb.Property(col => col.IdPerfil)
                  .ValueGeneratedOnAdd();

                tb.Property(col => col.Nombre).HasMaxLength(100).IsRequired();
                tb.ToTable("Perfil");

                tb.HasData(
                    new Perfil { IdPerfil = 1, Nombre = "Programador Dev" },
                    new Perfil { IdPerfil = 2, Nombre = "Programador Senior" },
                    new Perfil { IdPerfil = 3, Nombre = "Analista" }
                );
            });

            modelBuilder.Entity<Empleado>(tb =>
            {
                tb.HasKey(col => col.IdEmpleado);

                // CORRECCIÓN: Se elimina .UseIdentityColumn() por la misma razón.
                tb.Property(col => col.IdEmpleado)
                  .ValueGeneratedOnAdd();

                tb.Property(col => col.NombreCompleto).HasMaxLength(100).IsRequired();
                tb.ToTable("Empleado");

                tb.HasOne(col => col.PerfilReferencia)
                  .WithMany(col => col.EmpleadosReferencia)
                  .HasForeignKey(col => col.IdPerfil);
            });
        }
    }

}
