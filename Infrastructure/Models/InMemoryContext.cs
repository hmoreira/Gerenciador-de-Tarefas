using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskManager.Infrastructure.Models
{
    public partial class InMemoryContext : TaskManagerContext
    {        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("TaskManager");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.ToTable("Tarefa");

                entity.Property(e => e.DataVencimento).HasColumnType("date");

                entity.Property(e => e.Titulo).HasMaxLength(100);

                entity.HasOne(d => d.UsuarioCriador)
                    .WithMany(p => p.TarefaUsuarioCriadores)
                    .HasForeignKey(d => d.UsuarioCriadorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioCriador_Tarefa");

                entity.HasOne(d => d.UsuarioResponsavel)
                    .WithMany(p => p.TarefaUsuarioResponsaveis)
                    .HasForeignKey(d => d.UsuarioResponsavelId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsuarioResponsavel_Tarefa");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuario");

                entity.Property(e => e.Nome).HasMaxLength(100);

                entity.Property(e => e.Senha)
                    .HasMaxLength(64)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
