using System;
using System.Collections.Generic;
using MiBancaEnLineaAPI.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MiBancaEnLineaAPI.Data;

public partial class MiBancaEnLineaDbContext : DbContext
{
    public MiBancaEnLineaDbContext()
    {
    }

    public MiBancaEnLineaDbContext(DbContextOptions<MiBancaEnLineaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCliente> TblClientes { get; set; }

    public virtual DbSet<TblCuentaBancarium> TblCuentaBancaria { get; set; }

    public virtual DbSet<TblHistoricoSaldo> TblHistoricoSaldos { get; set; }

    public virtual DbSet<TblTasa> TblTasas { get; set; }

    public virtual DbSet<TblTipoTransaccion> TblTipoTransaccions { get; set; }

    public virtual DbSet<TblTransaccion> TblTransaccions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=MiBancaEnLineaDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCliente>(entity =>
        {
            entity.HasKey(e => e.PkTblCliente);

            entity.ToTable("TBL_CLIENTE");

            entity.Property(e => e.PkTblCliente).HasColumnName("PK_TBL_CLIENTE");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<TblCuentaBancarium>(entity =>
        {
            entity.HasKey(e => e.PkTblCuentaBancaria);

            entity.ToTable("TBL_CUENTA_BANCARIA");

            entity.Property(e => e.PkTblCuentaBancaria).HasColumnName("PK_TBL_CUENTA_BANCARIA");
            entity.Property(e => e.FkPkTblCliente).HasColumnName("FK_PK_TBL_CLIENTE");
            entity.Property(e => e.Saldo)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("SALDO");

            entity.HasOne(d => d.FkPkTblClienteNavigation).WithMany(p => p.TblCuentaBancaria)
                .HasForeignKey(d => d.FkPkTblCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PK_TBL_CLIENTE");
        });

        modelBuilder.Entity<TblHistoricoSaldo>(entity =>
        {
            entity.HasKey(e => e.PkTblHistoricoSaldos);

            entity.ToTable("TBL_HISTORICO_SALDOS");

            entity.Property(e => e.PkTblHistoricoSaldos).HasColumnName("PK_TBL_HISTORICO_SALDOS");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
            entity.Property(e => e.FkPkTblCuentaBancaria).HasColumnName("FK_PK_TBL_CUENTA_BANCARIA");
            entity.Property(e => e.InteresGanado)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("INTERES_GANADO");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("MONTO");
            entity.Property(e => e.TasaInteresDiario)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("TASA_INTERES_DIARIO");

            entity.HasOne(d => d.FkPkTblCuentaBancariaNavigation).WithMany(p => p.TblHistoricoSaldos)
                .HasForeignKey(d => d.FkPkTblCuentaBancaria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TBL_CUENTA_BANCARIA_TBL_HISTORICO_SALDOS");
        });

        modelBuilder.Entity<TblTasa>(entity =>
        {
            entity.HasKey(e => e.PkTblTasa);

            entity.ToTable("TBL_TASA");

            entity.Property(e => e.PkTblTasa).HasColumnName("PK_TBL_TASA");
            entity.Property(e => e.MontoDesde)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MONTO_DESDE");
            entity.Property(e => e.MontoHasta)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MONTO_HASTA");
            entity.Property(e => e.Tasa)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("TASA");
            entity.Property(e => e.TasaDiaria)
                .HasColumnType("decimal(18, 5)")
                .HasColumnName("TASA_DIARIA");
        });

        modelBuilder.Entity<TblTipoTransaccion>(entity =>
        {
            entity.HasKey(e => e.PkTblTipoTransaccion);

            entity.ToTable("TBL_TIPO_TRANSACCION");

            entity.Property(e => e.PkTblTipoTransaccion).HasColumnName("PK_TBL_TIPO_TRANSACCION");
            entity.Property(e => e.CodigoTipoTransaccion)
                .HasMaxLength(5)
                .HasColumnName("CODIGO_TIPO_TRANSACCION");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasColumnName("DESCRIPCION");
        });

        modelBuilder.Entity<TblTransaccion>(entity =>
        {
            entity.HasKey(e => e.PkTblTransaccion);

            entity.ToTable("TBL_TRANSACCION");

            entity.Property(e => e.PkTblTransaccion).HasColumnName("PK_TBL_TRANSACCION");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("FECHA");
            entity.Property(e => e.FkPkTblCuentaBancaria).HasColumnName("FK_PK_TBL_CUENTA_BANCARIA");
            entity.Property(e => e.FkPkTblCuentaBancariaDestino).HasColumnName("FK_PK_TBL_CUENTA_BANCARIA_DESTINO");
            entity.Property(e => e.FkPkTblTipoTransaccion).HasColumnName("FK_PK_TBL_TIPO_TRANSACCION");
            entity.Property(e => e.Monto)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("MONTO");

            entity.HasOne(d => d.FkPkTblCuentaBancariaNavigation).WithMany(p => p.TblTransaccionFkPkTblCuentaBancariaNavigations)
                .HasForeignKey(d => d.FkPkTblCuentaBancaria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PK_TBL_CUENTA_BANCARIA");

            entity.HasOne(d => d.FkPkTblCuentaBancariaDestinoNavigation).WithMany(p => p.TblTransaccionFkPkTblCuentaBancariaDestinoNavigations)
                .HasForeignKey(d => d.FkPkTblCuentaBancariaDestino)
                .HasConstraintName("FK_PK_TBL_CUENTA_BANCARIA_DESTINO");

            entity.HasOne(d => d.FkPkTblTipoTransaccionNavigation).WithMany(p => p.TblTransaccions)
                .HasForeignKey(d => d.FkPkTblTipoTransaccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PK_TBL_TIPO_TRANSACCION");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
