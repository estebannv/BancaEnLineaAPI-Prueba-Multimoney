using MiBancaEnLineaAPI.Util;

namespace MiBancaEnLineaAPI.Repositories.IRepositories
{
    public interface IInteresDiarioRepository
    {
        public Task<ResponseModel<bool>> CalculoInteresDiario();
    }
}
