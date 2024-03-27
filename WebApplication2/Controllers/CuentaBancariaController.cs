using Ganss.Xss;
using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Services.IServices;
using MiBancaEnLineaAPI.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MiBancaEnLineaAPI.Controllers
{
    [Route("api/cuenta-bancaria")]
    [ApiController]
    public class CuentaBancariaController : ControllerBase
    {
        private readonly ICuentaBancariaService _cuentaBancariaService;
        private readonly HtmlSanitizer _htmlSanitizer;

        public CuentaBancariaController(ICuentaBancariaService cuentaBancariaService, HtmlSanitizer htmlSanitizer)
        {
            _cuentaBancariaService = cuentaBancariaService;
            _htmlSanitizer = htmlSanitizer;
        }

        // EndPoint para obtener información de una cuenta bancaria por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> InformacionCuentaBancaria(string id)
        {
            try
            {
                // Sanitizar el ID de la cuenta bancaria para evitar ataques XSS
                var sanitizedId = _htmlSanitizer.Sanitize(id);

                // Verificar si el ID de la cuenta bancaria es válido
                if (!int.TryParse(sanitizedId, out int idCuenta) || idCuenta <= 0)
                {
                    return BadRequest(ResponseUtil.CreateResponse<CuentaBancaria>(null, "El id de cuenta bancaria no es válido", false));
                }

                // Consultar la información de la cuenta bancaria por su ID
                var response = await _cuentaBancariaService.ConsultaCuentaBancariaPorId(idCuenta);

                // Retornar la respuesta de la consulta
                if (response.EsValido)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseUtil.CreateResponse<CuentaBancaria>(null, $"Error: {ex.Message}", false));
            }
        }
    }
}
