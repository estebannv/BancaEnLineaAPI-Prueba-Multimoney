namespace MiBancaEnLineaAPI.Models
{
    public class CuentaBancaria
    {
        public int Id { get; set; }
        public int? IdCliente { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? InteresGanado { get; set; }
        public Cliente? Cliente { get; set; }
        public List<Transaccion>? Transacciones { get; set; }
    }
}
