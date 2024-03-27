namespace MiBancaEnLineaAPI.Util
{
    public class ResponseUtil
    {
        public static ResponseModel<T> CreateResponse<T>(object datos, string mensaje, bool esValido)
        {
            return new ResponseModel<T>
            {
                Datos = datos == null? default(T) : (T)datos,
                Mensaje = mensaje,
                EsValido = esValido
            };
        }
    }
}
