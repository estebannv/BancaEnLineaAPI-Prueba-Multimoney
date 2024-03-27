namespace MiBancaEnLineaAPI.Util
{
    public class ResponseModel<T>
    {
        public T? Datos { get; set; }
        public bool EsValido { get; set; }
        public string? Mensaje { get; set; }
    }
}
