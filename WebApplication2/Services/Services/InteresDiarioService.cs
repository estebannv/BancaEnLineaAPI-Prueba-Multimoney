using MiBancaEnLineaAPI.Data;
using MiBancaEnLineaAPI.Repositories.IRepositories;
using MiBancaEnLineaAPI.Services.IServices;
using MiBancaEnLineaAPI.Util;

namespace MiBancaEnLineaAPI.Services.Services
{
    public class InteresDiarioService : IInteresDiarioService
    {
        private readonly IInteresDiarioRepository _interesDiarioRepository;

        public InteresDiarioService(IInteresDiarioRepository interesDiarioRepository)
        {
            _interesDiarioRepository = interesDiarioRepository;
        }

        public async Task<ResponseModel<bool>> CalculoInteresDiario()
        {
            try
            {
                return await _interesDiarioRepository.CalculoInteresDiario();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
