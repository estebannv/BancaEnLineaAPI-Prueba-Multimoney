using MiBancaEnLineaAPI.Data.Models;
using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Util;

namespace MiBancaEnLineaAPI.Services.IServices
{
    public interface ITransaccionService
    {
        public Task<ResponseModel<bool>> RealizarDeposito(Transaccion transaccion);
        public Task<ResponseModel<bool>> RealizarRetiro(Transaccion transaccion);
        public Task<ResponseModel<bool>> RealizarTraspaso(Transaccion transaccion);
    }
}
