using MiBancaEnLineaAPI.Data;
using MiBancaEnLineaAPI.Repositories.IRepositories;
using MiBancaEnLineaAPI.Util;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MiBancaEnLineaAPI.Repositories.Repositories
{
    public class InteresDiarioRepository: IInteresDiarioRepository
    {
        private IConfiguration _config;

        public InteresDiarioRepository(IConfiguration config) {
            _config = config;
        }

        // Método para calcular el interés diario
        public async Task<ResponseModel<bool>> CalculoInteresDiario()
        {
            ResponseModel<bool> response;

            try
            {
                bool exitoso = false;

                // Establecer la conexión a la base de datos utilizando la cadena de conexión de la configuración
                using (SqlConnection conexion = new SqlConnection(_config.GetConnectionString("Connection")))
                {
                    string sp = "SP_CALCULAR_INTERES_DIARIO";
                    conexion.Open();
                    using (SqlCommand cmd = new SqlCommand(sp, conexion))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // El cálculo del interés diario se realizó con éxito
                            exitoso = true;
                        }
                    }
                }

                // Crear la respuesta basada en el resultado del cálculo del interés diario
                if (exitoso)
                {
                    response = ResponseUtil.CreateResponse<bool>(exitoso, "Interes diario calculado con éxito", true);
                }
                else
                {
                    response = ResponseUtil.CreateResponse<bool>(exitoso, "No se pudo calcular el interes diario", false);
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
