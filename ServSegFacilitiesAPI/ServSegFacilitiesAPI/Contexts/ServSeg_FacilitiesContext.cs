using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ServSegFacilitiesAPI.Domains;

namespace ServSegFacilitiesAPI.Contexts;

public partial class ServSeg_FacilitiesContext : DbContext
{
    public ServSeg_FacilitiesContext()
    {
    }

    public ServSeg_FacilitiesContext(DbContextOptions<ServSeg_FacilitiesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<cargo> cargo { get; set; }

    public virtual DbSet<empresa> empresa { get; set; }

    public virtual DbSet<historicoRegistroPonto> historicoRegistroPonto { get; set; }

    public virtual DbSet<localizacaoEmpresa> localizacaoEmpresa { get; set; }

    public virtual DbSet<registroPonto> registroPonto { get; set; }

    public virtual DbSet<tipoRegistro> tipoRegistro { get; set; }

    public virtual DbSet<usuario> usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                ?? "Host=aws-0-us-west-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.cdjbpblojmqqygapooch;Password=euamosenaiparasempre;SSL Mode=Require;Trust Server Certificate=true;";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<cargo>(entity =>
        {
            entity.HasKey(e => e.cargoId).HasName("PK__cargo__7E9F06A5A48E4FAA");

            entity.Property(e => e.nomeCargo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<empresa>(entity =>
        {
            entity.HasKey(e => e.empresaId).HasName("PK__empresa__C0E670799B6790B0");

            entity.HasIndex(e => e.cnpj, "UQ__empresa__35BD3E4883A638B5").IsUnique();

            entity.Property(e => e.bairro)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.cep)
                .HasMaxLength(9)
                .IsUnicode(false);
            entity.Property(e => e.cidade)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.cnpj)
                .HasMaxLength(18)
                .IsUnicode(false);
            entity.Property(e => e.complemento)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.estado)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.logradouro)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.nomeFantasia)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.numero)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.razaoSocial)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.telefone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<historicoRegistroPonto>(entity =>
        {
            entity.HasKey(e => e.historicoId).HasName("PK__historic__22DB9B754B653080");

            entity.HasOne(d => d.registroPontoEntrada).WithMany(p => p.historicoRegistroPontoregistroPontoEntrada)
                .HasForeignKey(d => d.registroPontoEntradaId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.registroPontoSaida).WithMany(p => p.historicoRegistroPontoregistroPontoSaida).HasForeignKey(d => d.registroPontoSaidaId);
        });

        modelBuilder.Entity<localizacaoEmpresa>(entity =>
        {
            entity.HasKey(e => e.localizacaoEmpresaId).HasName("PK__localiza__B3AFECF8131867C3");

            entity.Property(e => e.latitude)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.longitude)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.precisao).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.empresa).WithMany(p => p.localizacaoEmpresa)
                .HasForeignKey(d => d.empresaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<registroPonto>(entity =>
        {
            entity.HasKey(e => e.registroPontoId).HasName("PK__registro__F46A4ACFDC54DABF");

            entity.ToTable(tb => tb.HasTrigger("TR_registroPonto_Historico"));

            entity.Property(e => e.dataHoraPonto)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.tipoRegistro).WithMany(p => p.registroPonto)
                .HasForeignKey(d => d.tipoRegistroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegistroPonto_tipoRegistro_tipoRegistroId");

            entity.HasOne(d => d.usuario).WithMany(p => p.registroPonto)
                .HasForeignKey(d => d.usuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegistroPonto_usuario_usuarioId");
        });

        modelBuilder.Entity<tipoRegistro>(entity =>
        {
            entity.HasKey(e => e.tipoRegistroId).HasName("PK__tipoRegi__2058F4DCC2F460BE");

            entity.Property(e => e.nomeTipoRegistro)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.usuarioId).HasName("PK__usuario__A5B1AB8E809B4469");

            entity.HasIndex(e => e.email, "UQ__usuario__AB6E616404EE0C5D").IsUnique();

            entity.Property(e => e.email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.senha)
                .HasMaxLength(255);

            entity.HasOne(d => d.cargo).WithMany(p => p.usuario)
                .HasForeignKey(d => d.cargoId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.empresa).WithMany(p => p.usuario)
                .HasForeignKey(d => d.empresaId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
