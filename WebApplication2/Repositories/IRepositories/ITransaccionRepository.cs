using MiBancaEnLineaAPI.Data.Models;
using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Util;

namespace MiBancaEnLineaAPI.Repositories.IRepositories
{
    public interface ITransaccionRepository
    {
        public Task<ResponseModel<bool>> RealizarTransaccion(Transaccion transaccion);
    }
}
