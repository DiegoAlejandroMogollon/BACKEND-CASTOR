using Microsoft.EntityFrameworkCore;
using CASTOR_API.Models;

namespace CASTOR_API.Data // Asegúrate de que el espacio de nombres coincida
{
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Empleado> Empleados { get; set; }
    public DbSet<Cargo> Cargos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Cargo>()
        .ToTable("cargo")
        .Property(c => c.IdCargo)
        .HasColumnName("idcargo");

        modelBuilder.Entity<Cargo>()
        .Property(c => c.Nombre)
        .HasColumnName("nombre"); 

        modelBuilder.Entity<Empleado>()
        .ToTable("empleado")  
        .Property(e => e.Id)
        .HasColumnName("id");

         modelBuilder.Entity<Empleado>()
        .Property(e => e.Cedula)
        .HasColumnName("cedula");

        modelBuilder.Entity<Empleado>()
        .Property(e => e.Nombre)
        .HasColumnName("nombre");

         modelBuilder.Entity<Empleado>()
        .Property(e => e.FotoRuta)
        .HasColumnName("foto_ruta");

        modelBuilder.Entity<Empleado>()
        .Property(e => e.FechaIngreso)
        .HasColumnName("fechaingreso");

        modelBuilder.Entity<Empleado>()
        .Property(e => e.CargoId)
        .HasColumnName("cargo"); 
    }
}
}







