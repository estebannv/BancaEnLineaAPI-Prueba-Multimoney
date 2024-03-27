using MiBancaEnLineaAPI.Data.Models;
using MiBancaEnLineaAPI.Data;
using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Services.IServices;
using MiBancaEnLineaAPI.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using MiBancaEnLineaAPI.Util;

namespace MiBancaEnLineaAPI.Services.Services
{
    public class TransaccionService: ITransaccionService
    {
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly MiBancaEnLineaDbContext _context;

        public TransaccionService(ITransaccionRepository transaccionRepository, MiBancaEnLineaDbContext context)
        {
            _transaccionRepository = transaccionRepository;
            _context = context;
        }

        // Método para realizar un depósito
        public async Task<ResponseModel<bool>> RealizarDeposito(Transaccion transaccion)
        {
            ResponseModel<bool> response;

            try
            {
                // Validar que el monto sea válido
                if (transaccion.Monto <= 0)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Monto inválido", false);
                }

                // Verificar si la cuenta de origen existe en la base de datos
                bool cuentaOrigenExistente = await _context.TblCuentaBancaria
                    .AnyAsync(x => x.PkTblCuentaBancaria == transaccion.IdCuentaBancaria);

                if (!cuentaOrigenExistente)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Cuenta bancaria no existe", false);
                }

                // Establecer el tipo de transacción como depósito (1)
                transaccion.IdTipoTransaccion = 1;

                // Realizar la transacción utilizando el repositorio correspondiente
                response = await _transaccionRepository.RealizarTransaccion(transaccion);

            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        public async Task<ResponseModel<bool>> RealizarRetiro(Transaccion transaccion)
        {
            ResponseModel<bool> response;

            try
            {
                // Validar que el monto sea válido
                if (transaccion.Monto <= 0)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Monto inválido", false);
                }

                // Verificar si la cuenta de origen existe en la base de datos
                var cuentaOrigenExistente = await _context.TblCuentaBancaria
                    .FirstOrDefaultAsync(x => x.PkTblCuentaBancaria == transaccion.IdCuentaBancaria);

                if (cuentaOrigenExistente == null)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Cuenta bancaria no existe", false);
                }

                // Verificar si la cuenta tiene fondos suficientes para el retiro
                if (cuentaOrigenExistente.Saldo < transaccion.Monto)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Cuenta bancaria no posee fondos suficientes para realizar la transacción", false);
                }

                // Establecer el tipo de transacción como retiro (2)
                transaccion.IdTipoTransaccion = 2;

                // Realizar la transacción utilizando el repositorio correspondiente
                response = await _transaccionRepository.RealizarTransaccion(transaccion);

            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }

        // Método para realizar un traspaso
        public async Task<ResponseModel<bool>> RealizarTraspaso(Transaccion transaccion)
        {
            ResponseModel<bool> response;

            try
            {
                // Validar que el monto sea válido
                if (transaccion.Monto <= 0)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Monto inválido", false);
                }

                // Verificar que la cuenta de origen y destino sean diferentes
                if (transaccion.IdCuentaBancaria == transaccion.IdCuentaBancariaDestino)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Debe ingresar una cuenta de destino diferente a la de origen", false);
                }

                // Verificar que se haya ingresado una cuenta de destino válida
                if (transaccion.IdCuentaBancariaDestino == null || transaccion.IdCuentaBancariaDestino == 0)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Debe ingresar una cuenta de destino válida", false);
                }

                // Verificar si la cuenta de origen existe en la base de datos
                var cuentaOrigenExistente = await _context.TblCuentaBancaria
                    .FirstOrDefaultAsync(x => x.PkTblCuentaBancaria == transaccion.IdCuentaBancaria);

                // Verificar si la cuenta de destino existe en la base de datos
                bool cuentaDestinoExistente = await _context.TblCuentaBancaria
                    .AnyAsync(x => x.PkTblCuentaBancaria == transaccion.IdCuentaBancariaDestino);

                if (cuentaOrigenExistente == null)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Cuenta bancaria inexistente", false);
                }

                if (!cuentaDestinoExistente)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Cuenta bancaria de destino inexistente", false);
                }

                // Verificar si la cuenta tiene fondos suficientes para el retiro
                if (cuentaOrigenExistente.Saldo < transaccion.Monto)
                {
                    return ResponseUtil.CreateResponse<bool>(null, "Cuenta bancaria no posee fondos suficientes para realizar la transacción", false);
                }

                // Establecer el tipo de transacción como traspaso (3)
                transaccion.IdTipoTransaccion = 3;

                // Realizar la transacción utilizando el repositorio correspondiente
                response = await _transaccionRepository.RealizarTransaccion(transaccion);

            }
            catch (Exception)
            {
                throw;
            }

            return response;
        }
    }
}
