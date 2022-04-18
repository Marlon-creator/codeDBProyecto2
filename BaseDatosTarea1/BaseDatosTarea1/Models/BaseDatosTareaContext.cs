using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BaseDatosTarea1.Models
{
    public partial class BaseDatosTareaContext : DbContext
    {
        public BaseDatosTareaContext()
        {
        }

        public BaseDatosTareaContext(DbContextOptions<BaseDatosTareaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Departamento> Departamentos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Feriado> Feriados { get; set; } = null!;
        public virtual DbSet<Puesto> Puestos { get; set; } = null!;
        public virtual DbSet<TipoDocuIdentidad> TipoDocuIdentidads { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public IQueryable<Puesto> GetPuestos()
        {
            return this.Puestos.FromSqlRaw("EXECUTE dbo.ListarPuestos");
        }
        public IQueryable<Empleado> GetEmpleados()
        {
            return this.Empleados.FromSqlRaw("EXECUTE dbo.ListarEmpleados");
        }

        public IQueryable<Empleado> GetEmpleadosFiltro(SqlParameter nombre)
        {
            return this.Empleados.FromSqlRaw("EXECUTE dbo.ListarEmpleadosFiltro @inPatron", nombre);
        }

        
















        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Departamento>(entity =>
            {
                entity.ToTable("Departamento");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.ToTable("Empleado");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Puesto)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDepartamentoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdDepartamento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleado_Departamento");

                entity.HasOne(d => d.IdTipoDocuIdentidadNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdTipoDocuIdentidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Empleado_TipoDocuIdentidad");
            });

            modelBuilder.Entity<Feriado>(entity =>
            {
                entity.ToTable("Feriado");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Puesto>(entity =>
            {
                entity.ToTable("Puesto");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SalarioXhora)
                    .HasColumnType("money")
                    .HasColumnName("SalarioXHora");
            });

            modelBuilder.Entity<TipoDocuIdentidad>(entity =>
            {
                entity.ToTable("TipoDocuIdentidad");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
