using MiBancaEnLineaAPI.Services.IServices;
using MiBancaEnLineaAPI.Services.Services;

namespace MiBancaEnLineaAPI.Funtions
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<TimedHostedService> _logger;
        private readonly IInteresDiarioService _interesDiarioService;
        private Timer _timer;

        public TimedHostedService(ILogger<TimedHostedService> logger, IInteresDiarioService interesDiarioService)
        {
            _logger = logger;
            _interesDiarioService = interesDiarioService;
        }

        // Método para iniciar el servicio alojado.
        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service running.");

            // Se establece la ejecución del servicio cada 24 horas 
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(24));

            return Task.CompletedTask;
        }

        // Método que realiza el trabajo programado por el servicio alojado.
        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _interesDiarioService.CalculoInteresDiario();

            _logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }

        // Método para detener el servicio alojado.
        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Timed Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        // Método para liberar los recursos utilizados por el servicio alojado.
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
