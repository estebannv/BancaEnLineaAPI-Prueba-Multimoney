using Ganss.Xss;
using MiBancaEnLineaAPI.Controllers;
using MiBancaEnLineaAPI.Data;
using MiBancaEnLineaAPI.Models;
using MiBancaEnLineaAPI.Repositories.IRepositories;
using MiBancaEnLineaAPI.Repositories.Repositories;
using MiBancaEnLineaAPI.Services.IServices;
using MiBancaEnLineaAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace MiBancaEnLineaAPITest
{
    public class TransaccionTest
    {
        private readonly TransaccionController _controller;
        private readonly ITransaccionService _service;
        private readonly ITransaccionRepository _repository;
        private readonly MiBancaEnLineaDbContext _context;

        public TransaccionTest()
        {
            _context = new MiBancaEnLineaDbContext();
            _repository = new TransaccionRepository(_context);
            _service = new TransaccionService(_repository, _context);
            _controller = new TransaccionController(_service);
        }

        //TEST DEPOSITO

        #region DEPOSITO

        [Fact]
        public async void TransaccionDeposito_MontoValido_IdCuentaBancariaExistente_DevuelveMensajeValido()
        {
            IActionResult result = await _controller.RealizarDeposito(new Transaccion()
            {
                IdCuentaBancaria = 1,
                Monto = 20000
            });

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void TransaccionDeposito_MontoCero_IdCuentaBancariaExistente_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarDeposito(new Transaccion()
            {
                IdCuentaBancaria = 1,
                Monto = 0
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionDeposito_MontoNegativo_IdCuentaBancariaExistente_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarDeposito(new Transaccion()
            {
                IdCuentaBancaria = 1,
                Monto = -150
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionDeposito_MontoValido_IdCuentaBancariaNula_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarDeposito(new Transaccion()
            {
                Monto = 20000
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionDeposito_MontoNulo_IdCuentaBancariaExistente_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarDeposito(new Transaccion()
            {
                IdCuentaBancaria = 1
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        //TEST RETIRO

        #region RETIRO

        [Fact]
        public async void TransaccionRetiro_MontoValido_IdCuentaBancariaExistente_DevuelveMensajeValido()
        {
            IActionResult result = await _controller.RealizarRetiro(new Transaccion()
            {
                IdCuentaBancaria = 1,
                Monto = 20000
            });

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void TransaccionRetiro_MontoCero_IdCuentaBancariaExistente_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarRetiro(new Transaccion()
            {
                IdCuentaBancaria = 1,
                Monto = 0
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionRetiro_MontoNegativo_IdCuentaBancariaExistente_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarRetiro(new Transaccion()
            {
                IdCuentaBancaria = 1,
                Monto = -150
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionRetiro_MontoValido_IdCuentaBancariaNula_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarRetiro(new Transaccion()
            {
                Monto = 20000
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionRetiro_MontoNulo_IdCuentaBancariaExistente_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarRetiro(new Transaccion()
            {
                IdCuentaBancaria = 1
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionRetiro_MontoMayorAlSaldoCuenta_IdCuentaBancariaExistente_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarRetiro(new Transaccion()
            {
                IdCuentaBancaria = 1,
                Monto = 1000000
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        //TEST TRASPASO

        #region TRASPASO

        [Fact]
        public async void TransaccionTraspaso_MontoValido_IdCuentaBancariaOrigenDestinoExistentes_DevuelveMensajeValido()
        {
            IActionResult result = await _controller.RealizarTraspaso(new Transaccion()
            {
                IdCuentaBancaria = 1,
                IdCuentaBancariaDestino = 2,
                Monto = 20000
            });

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void TransaccionTraspaso_MontoValido_IdCuentaBancariaOrigenDestinoNoExistentes_DevuelveMensajeValido()
        {
            IActionResult result = await _controller.RealizarTraspaso(new Transaccion()
            {
                IdCuentaBancaria = 50,
                IdCuentaBancariaDestino = 30,
                Monto = 20000
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionTraspaso_MontoCero_IdCuentaBancariaOrigenDestinoExistentes_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarTraspaso(new Transaccion()
            {
                IdCuentaBancaria = 1,
                IdCuentaBancariaDestino = 2,
                Monto = 0
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionTraspaso_MontoNegativo_IdCuentaBancariaOrigenDestinoExistentes_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarTraspaso(new Transaccion()
            {
                IdCuentaBancaria = 1,
                IdCuentaBancariaDestino = 2,
                Monto = -150
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionTraspaso_MontoValido_IdCuentaBancariaOrigenNula_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarTraspaso(new Transaccion()
            {
                IdCuentaBancariaDestino = 2,
                Monto = 5000
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionTraspaso_MontoValido_IdCuentaBancariaDestinoNula_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarTraspaso(new Transaccion()
            {
                IdCuentaBancaria = 1,
                Monto = 5000
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionTraspaso_MontoNulo_IdCuentaBancariaOrigenDestinoExistentes_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarTraspaso(new Transaccion()
            {
                IdCuentaBancaria = 1,
                IdCuentaBancariaDestino = 2
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void TransaccionTraspaso_MontoMayorAlSaldoCuenta_IdCuentaBancariaOrigenDestinoExistentes_DevuelveMensajeInvalido()
        {
            IActionResult result = await _controller.RealizarTraspaso(new Transaccion()
            {
                IdCuentaBancaria = 1,
                IdCuentaBancariaDestino = 2,
                Monto = 1000000
            });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

    }
}