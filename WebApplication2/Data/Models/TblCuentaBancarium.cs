using System;
using System.Collections.Generic;

namespace MiBancaEnLineaAPI.Data.Models;

public partial class TblCuentaBancarium
{
    public int PkTblCuentaBancaria { get; set; }

    public int FkPkTblCliente { get; set; }

    public decimal Saldo { get; set; }

    public virtual TblCliente FkPkTblClienteNavigation { get; set; } = null!;

    public virtual ICollection<TblHistoricoSaldo> TblHistoricoSaldos { get; set; } = new List<TblHistoricoSaldo>();

    public virtual ICollection<TblTransaccion> TblTransaccionFkPkTblCuentaBancariaDestinoNavigations { get; set; } = new List<TblTransaccion>();

    public virtual ICollection<TblTransaccion> TblTransaccionFkPkTblCuentaBancariaNavigations { get; set; } = new List<TblTransaccion>();
}
