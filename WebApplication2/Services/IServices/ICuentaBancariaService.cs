using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Util;

namespace MiBancaEnLineaAPI.Services.IServices
{
    public interface ICuentaBancariaService
    {
        public Task<ResponseModel<CuentaBancaria>> ConsultaCuentaBancariaPorId(int id);
    }
}
