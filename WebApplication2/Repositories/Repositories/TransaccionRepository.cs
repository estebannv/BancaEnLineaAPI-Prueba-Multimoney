using MiBancaEnLineaAPI.Data;
using MiBancaEnLineaAPI.Data.Models;
using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Repositories.IRepositories;
using MiBancaEnLineaAPI.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MiBancaEnLineaAPI.Repositories.Repositories
{
    public class TransaccionRepository: ITransaccionRepository
    {
        private readonly MiBancaEnLineaDbContext _context;

        public TransaccionRepository(MiBancaEnLineaDbContext context) { 
            _context = context;
        }

        // Método para realizar transacciones
        public async Task<ResponseModel<bool>> RealizarTransaccion(Transaccion transaccion)
        {
            ResponseModel<bool> response;

            try
            {
                // Definición de los parámetros para la consulta SQL
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@IdCuentaBancaria", transaccion.IdCuentaBancaria),
                    new SqlParameter("@IdCuentaBancariaDestino", transaccion.IdCuentaBancariaDestino),
                    new SqlParameter("@IdTipoTransaccion", transaccion.IdTipoTransaccion),
                    new SqlParameter("@Monto", transaccion.Monto),
                };

                // Consulta SQL para ejecutar la transacción
                var query = @"
                    EXEC SP_MAN_TRANSACCION
                        @P_PK_TBL_CUENTA_BANCARIA  = @IdCuentaBancaria, 
                        @P_PK_TBL_CUENTA_BANCARIA_DESTINO  = @IdCuentaBancariaDestino, 
                        @P_PK_TBL_TIPO_TRANSACCION  = @IdTipoTransaccion, 
                        @P_MONTO  = @Monto
                ";

                // Ejecutar la consulta SQL utilizando ExecuteSqlRawAsync
                var result = await _context.Database.ExecuteSqlRawAsync(query, parameters);

                // Verificar si la transacción fue exitosa
                bool exitoso = result > 0;

                // Crear la respuesta basada en el resultado de la transacción
                if (exitoso)
                {
                    response = ResponseUtil.CreateResponse<bool>(exitoso, "Transacción realizada correctamente", true);
                }
                else
                {
                    response = ResponseUtil.CreateResponse<bool>(exitoso, "No se pudo realizar la transacción", false);
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
