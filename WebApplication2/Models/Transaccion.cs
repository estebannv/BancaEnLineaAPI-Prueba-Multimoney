using System.ComponentModel.DataAnnotations;

namespace MiBancaEnLineaAPI.Models
{
    public class Transaccion
    {
        public Transaccion()
        {
            IdCuentaBancariaDestino = 0;
        }

        public int? Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El número de cuenta bancaria es inválido")]
        [Required(ErrorMessage = "El número de cuenta bancaria es requerido")]
        public int IdCuentaBancaria { get; set; }
        public int? IdCuentaBancariaDestino { get; set; }
        public int? IdTipoTransaccion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El monto debe ser mayor que 0")]
        [Required(ErrorMessage = "El monto es requerido")]
        public decimal Monto { get; set; }
        public DateTime? Fecha { get; set; }
        public string? TitularCuentaBancaria { get; set; }
        public string? TitularCuentaBancariaDestino { get; set; }
        public string? DetalleTipoTransaccion { get; set; }
    }
}
