using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiPruebaTecnica.Models;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=BdGodoyCordoba");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Cedula).HasName("Pk_Usuarios");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D1053458E51188").IsUnique();

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
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
