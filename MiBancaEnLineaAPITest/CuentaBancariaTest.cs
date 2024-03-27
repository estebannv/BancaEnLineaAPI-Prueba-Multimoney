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
    public class CuentaBancariaTest
    {
        private readonly CuentaBancariaController _controller;
        private readonly ICuentaBancariaService _service;
        private readonly ICuentaBancariaRepository _repository;
        private readonly MiBancaEnLineaDbContext _context;

        public CuentaBancariaTest()
        {
            HtmlSanitizer htmlSanitizer = new HtmlSanitizer();
            _context = new MiBancaEnLineaDbContext();
            _repository = new CuentaBancariaRepository(_context);
            _service = new CuentaBancariaService(_repository);
            _controller = new CuentaBancariaController(_service, htmlSanitizer);
        }

        [Fact]
        public async void ObtenerInformacionCuenta_IdExistente_DevuelveMensajeValido()
        {
            IActionResult result = await _controller.InformacionCuentaBancaria("1");

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ObtenerInformacionCuenta_IdNoExistente_DevuelveMensajeInvalidoAsync()
        {
            IActionResult result = await _controller.InformacionCuentaBancaria("1515");

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ObtenerInformacionCuenta_IdLetras_DevuelveMensajeInvalidoAsync()
        {
            IActionResult result = await _controller.InformacionCuentaBancaria("CARLOS");

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}