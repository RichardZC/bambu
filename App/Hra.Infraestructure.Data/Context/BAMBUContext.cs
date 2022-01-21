using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Hra.Domain.Entity;

namespace Hra.Infraestructure.Data
{
    public partial class BAMBUContext : DbContext
    {
        public BAMBUContext()
        {
        }

        public BAMBUContext(DbContextOptions<BAMBUContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Boveda> Boveda { get; set; } = null!;
        public virtual DbSet<BovedaMov> BovedaMov { get; set; } = null!;
        public virtual DbSet<Caja> Caja { get; set; } = null!;
        public virtual DbSet<CajaDiario> CajaDiario { get; set; } = null!;
        public virtual DbSet<Cliente> Cliente { get; set; } = null!;
        public virtual DbSet<Menu> Menu { get; set; } = null!;
        public virtual DbSet<MovimientoCaja> MovimientoCaja { get; set; } = null!;
        public virtual DbSet<MovimientoCajaAnu> MovimientoCajaAnu { get; set; } = null!;
        public virtual DbSet<Ocupacion> Ocupacion { get; set; } = null!;
        public virtual DbSet<Persona> Persona { get; set; } = null!;
        public virtual DbSet<Rol> Rol { get; set; } = null!;
        public virtual DbSet<RolMenu> RolMenu { get; set; } = null!;
        public virtual DbSet<TipoOperacion> TipoOperacion { get; set; } = null!;
        public virtual DbSet<Usuario> Usuario { get; set; } = null!;
        public virtual DbSet<UsuarioRol> UsuarioRol { get; set; } = null!;
        public virtual DbSet<ValorTabla> ValorTabla { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=connectionDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boveda>(entity =>
            {
                entity.ToTable("Boveda", "CAJA");

                entity.Property(e => e.Entradas).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.FechaFinOperacion).HasColumnType("datetime");

                entity.Property(e => e.FechaIniOperacion).HasColumnType("datetime");

                entity.Property(e => e.SaldoFinal).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.Salidas).HasColumnType("decimal(16, 2)");
            });

            modelBuilder.Entity<BovedaMov>(entity =>
            {
                entity.HasKey(e => e.MovimientoBovedaId)
                    .HasName("PK__BovedaMo__BF3A00B06F7AE345");

                entity.ToTable("BovedaMov", "CAJA");

                entity.Property(e => e.CodOperacion)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.FechaReg).HasColumnType("datetime");

                entity.Property(e => e.Glosa).IsUnicode(false);

                entity.Property(e => e.Importe).HasColumnType("decimal(16, 2)");

                entity.HasOne(d => d.Boveda)
                    .WithMany(p => p.BovedaMov)
                    .HasForeignKey(d => d.BovedaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BovedaMov__Boved__5812160E");
            });

            modelBuilder.Entity<Caja>(entity =>
            {
                entity.ToTable("Caja", "CAJA");

                entity.Property(e => e.Denominacion)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CajaDiario>(entity =>
            {
                entity.ToTable("CajaDiario", "CAJA");

                entity.Property(e => e.Entradas).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.FechaFinOperacion).HasColumnType("datetime");

                entity.Property(e => e.FechaIniOperacion).HasColumnType("datetime");

                entity.Property(e => e.SaldoFinal).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.SaldoInicial).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.Salidas).HasColumnType("decimal(16, 2)");

                entity.HasOne(d => d.Caja)
                    .WithMany(p => p.CajaDiario)
                    .HasForeignKey(d => d.CajaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CajaDiari__CajaI__403A8C7D");

                entity.HasOne(d => d.UsuarioAsignado)
                    .WithMany(p => p.CajaDiario)
                    .HasForeignKey(d => d.UsuarioAsignadoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CajaDiari__Usuar__412EB0B6");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente", "MAESTRO");

                entity.Property(e => e.ClienteId).ValueGeneratedNever();

                entity.Property(e => e.Calificacion)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DireccionNegocio)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionNegocioRef)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.FechaRegistro).HasColumnType("datetime");

                entity.Property(e => e.Nota)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cliente__Persona__3B75D760");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu", "MAESTRO");

                entity.Property(e => e.Denominacion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Icono)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Modulo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Orden).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.Referencia).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MovimientoCaja>(entity =>
            {
                entity.ToTable("MovimientoCaja", "CAJA");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.FechaReg).HasColumnType("datetime");

                entity.Property(e => e.ImportePago).HasColumnType("decimal(16, 2)");

                entity.Property(e => e.Operacion)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.CajaDiario)
                    .WithMany(p => p.MovimientoCaja)
                    .HasForeignKey(d => d.CajaDiarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Movimient__CajaD__4CA06362");

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.MovimientoCaja)
                    .HasForeignKey(d => d.PersonaId)
                    .HasConstraintName("FK__Movimient__Perso__4E88ABD4");
            });

            modelBuilder.Entity<MovimientoCajaAnu>(entity =>
            {
                entity.ToTable("MovimientoCajaAnu", "CAJA");

                entity.Property(e => e.FechaReg).HasColumnType("datetime");

                entity.Property(e => e.Observacion).IsUnicode(false);

                entity.HasOne(d => d.MovimientoCaja)
                    .WithMany(p => p.MovimientoCajaAnu)
                    .HasForeignKey(d => d.MovimientoCajaId)
                    .HasConstraintName("FK__Movimient__Movim__534D60F1");
            });

            modelBuilder.Entity<Ocupacion>(entity =>
            {
                entity.ToTable("Ocupacion", "MAESTRO");

                entity.Property(e => e.Denominacion)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona", "MAESTRO");

                entity.Property(e => e.ApeMaterno)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.ApePaterno)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.Celular)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Conyugue)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ConyugueCelular)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ConyugueDni)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("ConyugueDNI");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionRef)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FechaNacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroDocumento)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TipoDocumento)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TipoPersona)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol", "MAESTRO");

                entity.Property(e => e.Denominacion)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolMenu>(entity =>
            {
                entity.ToTable("RolMenu", "MAESTRO");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.RolMenu)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK__RolMenu__MenuId__38996AB5");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.RolMenu)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK__RolMenu__RolId__37A5467C");
            });

            modelBuilder.Entity<TipoOperacion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TipoOperacion", "MAESTRO");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Denominacion)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario", "MAESTRO");

                entity.Property(e => e.UsuarioId).ValueGeneratedNever();

                entity.Property(e => e.Clave).HasMaxLength(50);

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuario__Persona__286302EC");
            });

            modelBuilder.Entity<UsuarioRol>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("UsuarioRol", "MAESTRO");

                entity.Property(e => e.UsuarioRolId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Rol)
                    .WithMany()
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK__UsuarioRo__RolId__2B3F6F97");

                entity.HasOne(d => d.Usuario)
                    .WithMany()
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK__UsuarioRo__Usuar__2A4B4B5E");
            });

            modelBuilder.Entity<ValorTabla>(entity =>
            {
                entity.HasKey(e => new { e.TablaId, e.ItemId })
                    .HasName("PK_VALORTABLA");

                entity.ToTable("ValorTabla", "MAESTRO");

                entity.Property(e => e.Denominacion)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.DesCorta)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Valor)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
