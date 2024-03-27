using System;
using System.Collections.Generic;

namespace MiBancaEnLineaAPI.Data.Models;

public partial class TblHistoricoSaldo
{
    public int PkTblHistoricoSaldos { get; set; }

    public DateTime Fecha { get; set; }

    public int FkPkTblCuentaBancaria { get; set; }

    public decimal? TasaInteresDiario { get; set; }

    public decimal? Monto { get; set; }

    public decimal? InteresGanado { get; set; }

    public virtual TblCuentaBancarium FkPkTblCuentaBancariaNavigation { get; set; } = null!;
}
