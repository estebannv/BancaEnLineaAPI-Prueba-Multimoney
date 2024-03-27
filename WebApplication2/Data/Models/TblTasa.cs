using System;
using System.Collections.Generic;

namespace MiBancaEnLineaAPI.Data.Models;

public partial class TblTasa
{
    public int PkTblTasa { get; set; }

    public decimal MontoDesde { get; set; }

    public decimal? MontoHasta { get; set; }

    public decimal Tasa { get; set; }

    public decimal? TasaDiaria { get; set; }
}
