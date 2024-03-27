using MiBancaEnLineaAPI.Util;

namespace MiBancaEnLineaAPI.Services.IServices
{
    public interface IInteresDiarioService
    {
        public Task<ResponseModel<bool>> CalculoInteresDiario();
    }
}
