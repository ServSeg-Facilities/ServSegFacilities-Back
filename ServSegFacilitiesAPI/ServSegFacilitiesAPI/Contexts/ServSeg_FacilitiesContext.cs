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

    public virtual DbSet<localizacaoEmpresa> localizacaoEmpresa { get; set; }

    public virtual DbSet<registroPonto> registroPonto { get; set; }

    public virtual DbSet<tipoRegistro> tipoRegistro { get; set; }

    public virtual DbSet<usuario> usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ServSeg_Facilities;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<cargo>(entity =>
        {
            entity.HasKey(e => e.cargoId).HasName("PK__cargo__7E9F06A555767DA0");

            entity.Property(e => e.nomeCargo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<empresa>(entity =>
        {
            entity.HasKey(e => e.empresaId).HasName("PK__empresa__C0E670791E9A12A1");

            entity.HasIndex(e => e.cnpj, "UQ__empresa__35BD3E4878DABFAB").IsUnique();

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

        modelBuilder.Entity<localizacaoEmpresa>(entity =>
        {
            entity.HasKey(e => e.localizacaoEmpresaId).HasName("PK__localiza__B3AFECF800840F08");

            entity.Property(e => e.latitude)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.longitude)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.precisao).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.empresa).WithMany(p => p.localizacaoEmpresa)
                .HasForeignKey(d => d.empresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__localizac__empre__6B24EA82");
        });

        modelBuilder.Entity<registroPonto>(entity =>
        {
            entity.HasKey(e => e.registroPontoId).HasName("PK__registro__F46A4ACF051BC8BB");

            entity.Property(e => e.dataHoraPonto)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.tipoRegistro).WithMany(p => p.registroPonto)
                .HasForeignKey(d => d.tipoRegistroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__registroP__tipoR__6D0D32F4");

            entity.HasOne(d => d.usuario).WithMany(p => p.registroPonto)
                .HasForeignKey(d => d.usuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__registroP__usuar__6C190EBB");
        });

        modelBuilder.Entity<tipoRegistro>(entity =>
        {
            entity.HasKey(e => e.tipoRegistroId).HasName("PK__tipoRegi__2058F4DC6519BD82");

            entity.Property(e => e.nomeTipoRegistro)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.usuarioId).HasName("PK__usuario__A5B1AB8EB7978B33");

            entity.HasIndex(e => e.email, "UQ__usuario__AB6E61647D30917E").IsUnique();

            entity.Property(e => e.email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.senha)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.cargo).WithMany(p => p.usuario)
                .HasForeignKey(d => d.cargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usuario__cargoId__693CA210");

            entity.HasOne(d => d.empresa).WithMany(p => p.usuario)
                .HasForeignKey(d => d.empresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usuario__empresa__6A30C649");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
