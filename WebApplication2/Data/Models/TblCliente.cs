using System;
using System.Collections.Generic;

namespace MiBancaEnLineaAPI.Data.Models;

public partial class TblCliente
{
    public int PkTblCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<TblCuentaBancarium> TblCuentaBancaria { get; set; } = new List<TblCuentaBancarium>();
}
