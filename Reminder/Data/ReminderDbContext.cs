using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Reminder.Models;

namespace Reminder.Data;

public partial class ReminderDbContext : DbContext
{
    public ReminderDbContext(DbContextOptions<ReminderDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Evento> Eventos { get; set; }
    public virtual DbSet<EventosxUsuarios> EventosxUsuarios { get; set; }
    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Eventos__3214EC07C4D213B1");

            entity.Property(e => e.Criacao).HasColumnType("datetime");
            entity.Property(e => e.Horario).HasColumnType("timespan");
            entity.Property(e => e.Descricao).HasColumnType("text");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<EventosxUsuarios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Responsa__3214EC075125CB6B");

            entity.Property(e => e.IdEvento).HasColumnName("Id_evento");
            entity.Property(e => e.IdUsuario).HasColumnName("Id_usuario");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC0779933297");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
