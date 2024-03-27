using MiBancaEnLineaAPI.Data;
using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Repositories.IRepositories;
using MiBancaEnLineaAPI.Util;
using Microsoft.EntityFrameworkCore;

namespace MiBancaEnLineaAPI.Repositories.Repositories
{
    public class CuentaBancariaRepository: ICuentaBancariaRepository
    {
        private readonly MiBancaEnLineaDbContext _context;

        public CuentaBancariaRepository(MiBancaEnLineaDbContext context)
        {
            _context = context;
        }

        // Método para consultar una cuenta bancaria por su ID
        public async Task<ResponseModel<CuentaBancaria>> ConsultaCuentaBancariaPorId(int id)
        {
            ResponseModel<CuentaBancaria> response;

            try
            {
                // Realizar la consulta para obtener los detalles de la cuenta bancaria
                var result = await _context.TblCuentaBancaria
                        .Where(x => x.PkTblCuentaBancaria == id)
                        .Include(y => y.FkPkTblClienteNavigation) // Incluir la información del cliente asociado a la cuenta bancaria
                        .Select(c => new CuentaBancaria
                        {
                            Id = c.PkTblCuentaBancaria,
                            Saldo = c.Saldo,
                            InteresGanado = c.TblHistoricoSaldos.Sum(y => y.InteresGanado), // Calcular el interés ganado de la cuenta bancaria
                            Cliente = new Cliente
                            {
                                Id = c.FkPkTblCliente,
                                Nombre = c.FkPkTblClienteNavigation.Nombre,
                                Apellido = c.FkPkTblClienteNavigation.Apellido,
                                Email = c.FkPkTblClienteNavigation.Email,
                            },
                            Transacciones = _context.TblTransaccions
                            .Where(t => t.FkPkTblCuentaBancaria == c.PkTblCuentaBancaria) // Obtener las transacciones asociadas a la cuenta bancaria
                            .Select(t => new Transaccion
                            {
                                Id = t.PkTblTransaccion,
                                Monto = t.Monto,
                                Fecha = t.Fecha,
                                IdCuentaBancaria = t.FkPkTblCuentaBancaria,
                                IdCuentaBancariaDestino = t.FkPkTblCuentaBancariaDestino,
                                IdTipoTransaccion = t.FkPkTblTipoTransaccion,
                                TitularCuentaBancaria = c.FkPkTblClienteNavigation.Nombre,
                                TitularCuentaBancariaDestino = t.FkPkTblCuentaBancariaDestinoNavigation.FkPkTblClienteNavigation.Nombre,
                                DetalleTipoTransaccion = t.FkPkTblTipoTransaccionNavigation.Descripcion,
                            })
                            .ToList()
                        })
                        .FirstOrDefaultAsync();

                bool exitoso = result != null;

                // Crear la respuesta basada en el resultado de la consulta
                if (exitoso)
                {
                    response = ResponseUtil.CreateResponse<CuentaBancaria>(result, "Cuenta bancaria encontrada con éxito", true);
                }
                else
                {
                    response = ResponseUtil.CreateResponse<CuentaBancaria>(null, "Cuenta bancaria no existe", false);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }
    }
}
