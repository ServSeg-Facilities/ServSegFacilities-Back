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
        => optionsBuilder.UseSqlServer("Server=(localDb)\\MSSQLLocalDb; Database=ServSeg_Facilities; Trusted_Connection=true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<cargo>(entity =>
        {
            entity.HasKey(e => e.cargoId).HasName("PK__cargo__7E9F06A53A67EC41");

            entity.Property(e => e.nomeCargo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<empresa>(entity =>
        {
            entity.HasKey(e => e.empresaId).HasName("PK__empresa__C0E6707914CA982A");

            entity.HasIndex(e => e.cnpj, "UQ__empresa__35BD3E48360B4CCD").IsUnique();

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
            entity.HasKey(e => e.localizacaoEmpresaId).HasName("PK__localiza__B3AFECF8C9B92E9B");

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
                .HasConstraintName("FK__localizac__empre__59063A47");
        });

        modelBuilder.Entity<registroPonto>(entity =>
        {
            entity.HasKey(e => e.registroPontoId).HasName("PK__registro__F46A4ACF4EB2DBA4");

            entity.Property(e => e.dataHoraPonto)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.tipoRegistro).WithMany(p => p.registroPonto)
                .HasForeignKey(d => d.tipoRegistroId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__registroP__tipoR__5AEE82B9");

            entity.HasOne(d => d.usuario).WithMany(p => p.registroPonto)
                .HasForeignKey(d => d.usuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__registroP__usuar__59FA5E80");
        });

        modelBuilder.Entity<tipoRegistro>(entity =>
        {
            entity.HasKey(e => e.tipoRegistroId).HasName("PK__tipoRegi__2058F4DC41643959");

            entity.Property(e => e.nomeTipoRegistro)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<usuario>(entity =>
        {
            entity.HasKey(e => e.usuarioId).HasName("PK__usuario__A5B1AB8EAA2C136A");

            entity.HasIndex(e => e.email, "UQ__usuario__AB6E6164554833CC").IsUnique();

            entity.Property(e => e.email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.nome)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.senha).HasMaxLength(32);

            entity.HasOne(d => d.cargo).WithMany(p => p.usuario)
                .HasForeignKey(d => d.cargoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usuario__cargoId__571DF1D5");

            entity.HasOne(d => d.empresa).WithMany(p => p.usuario)
                .HasForeignKey(d => d.empresaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usuario__empresa__5812160E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
