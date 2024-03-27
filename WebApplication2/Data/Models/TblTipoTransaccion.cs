using System;
using System.Collections.Generic;

namespace MiBancaEnLineaAPI.Data.Models;

public partial class TblTipoTransaccion
{
    public int PkTblTipoTransaccion { get; set; }

    public string CodigoTipoTransaccion { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<TblTransaccion> TblTransaccions { get; set; } = new List<TblTransaccion>();
}
