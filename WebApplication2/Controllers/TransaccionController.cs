using Azure;
using Ganss.Xss;
using MiBancaEnLineaAPI.Data.Models;
using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Services.IServices;
using MiBancaEnLineaAPI.Services.Services;
using MiBancaEnLineaAPI.Util;
using Microsoft.AspNetCore.Mvc;

namespace MiBancaEnLineaAPI.Controllers
{
    [Route("api/transaccion")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;

        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        // Endpoint para realizar un depósito
        [HttpPost("deposito")]
        public async Task<IActionResult> RealizarDeposito([FromBody] Transaccion transaccion)
        {
            try
            {
                // Validación del modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Realizar el depósito utilizando el servicio correspondiente
                var response = await _transaccionService.RealizarDeposito(transaccion);

                // Retornar la respuesta de la operación
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

        // Endpoint para realizar un retiro
        [HttpPost("retiro")]
        public async Task<IActionResult> RealizarRetiro([FromBody] Transaccion transaccion)
        {
            try
            {
                // Validación del modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Realizar el retiro utilizando el servicio correspondiente
                var response = await _transaccionService.RealizarRetiro(transaccion);

                // Retornar la respuesta de la operación
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

        // Endpoint para realizar un traspaso
        [HttpPost("traspaso")]
        public async Task<IActionResult> RealizarTraspaso([FromBody] Transaccion transaccion)
        {
            try
            {
                // Validación del modelo
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Realizar el traspaso utilizando el servicio correspondiente
                var response = await _transaccionService.RealizarTraspaso(transaccion);

                // Retornar la respuesta de la operación
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
