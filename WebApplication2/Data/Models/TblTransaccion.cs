using System;
using System.Collections.Generic;

namespace MiBancaEnLineaAPI.Data.Models;

public partial class TblTransaccion
{
    public int PkTblTransaccion { get; set; }

    public int FkPkTblCuentaBancaria { get; set; }

    public int? FkPkTblCuentaBancariaDestino { get; set; }

    public int FkPkTblTipoTransaccion { get; set; }

    public decimal Monto { get; set; }

    public DateTime Fecha { get; set; }

    public virtual TblCuentaBancarium? FkPkTblCuentaBancariaDestinoNavigation { get; set; }

    public virtual TblCuentaBancarium FkPkTblCuentaBancariaNavigation { get; set; } = null!;

    public virtual TblTipoTransaccion FkPkTblTipoTransaccionNavigation { get; set; } = null!;
}
