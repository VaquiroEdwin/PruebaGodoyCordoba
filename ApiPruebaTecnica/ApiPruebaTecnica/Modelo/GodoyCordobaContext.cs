using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiPruebaTecnica.Modelo;

public partial class GodoyCordobaContext : DbContext
{
    public GodoyCordobaContext()
    {
    }

    public GodoyCordobaContext(DbContextOptions<GodoyCordobaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Cedula).HasName("Pk_Usuarios");

            entity.Property(e => e.Cedula).ValueGeneratedNever();
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaAcceso).HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
