using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Util;

namespace MiBancaEnLineaAPI.Repositories.IRepositories
{
    public interface ICuentaBancariaRepository
    {
        public Task<ResponseModel<CuentaBancaria>> ConsultaCuentaBancariaPorId(int id);
    }
}
