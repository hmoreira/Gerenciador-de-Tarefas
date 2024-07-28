using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TaskManager.Infrastructure.Models
{
    public partial class SQLServerContext : TaskManagerContext
    {
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=sql-server;Initial Catalog=TaskManager;User Id=sa;Password=97Hebmor@;TrustServerCertificate=True;Encrypt=True");
                //optionsBuilder.UseSqlServer("Data Source=localhost,1455\\sql1;Initial Catalog=TaskManager;User Id=sa;Password=97Hebmor@;TrustServerCertificate=True;Encrypt=True");
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
                entity.Property(e => e.Username).HasMaxLength(50);

                entity.Property(e => e.Senha)
                    .HasMaxLength(64)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
