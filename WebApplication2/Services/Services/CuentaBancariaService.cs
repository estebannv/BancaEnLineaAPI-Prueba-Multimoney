using MiBancaEnLineaAPI.Data;
using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Repositories.IRepositories;
using MiBancaEnLineaAPI.Services.IServices;
using MiBancaEnLineaAPI.Util;

namespace MiBancaEnLineaAPI.Services.Services
{
    public class CuentaBancariaService: ICuentaBancariaService
    {
        private readonly ICuentaBancariaRepository _cuentaBancariaRepository;

        public CuentaBancariaService(ICuentaBancariaRepository cuentaBancariaRepository)
        {
            _cuentaBancariaRepository = cuentaBancariaRepository;
        }

        public async Task<ResponseModel<CuentaBancaria>> ConsultaCuentaBancariaPorId(int id)
        {
            try
            {
                return await _cuentaBancariaRepository.ConsultaCuentaBancariaPorId(id);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
