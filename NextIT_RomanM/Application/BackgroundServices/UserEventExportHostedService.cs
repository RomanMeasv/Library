using Azure.Security.KeyVault.Certificates;
using NextIT_RomanM.Core.Application.Services;
using NextIT_RomanM.Core.Domain.Interfaces;

namespace NextIT_RomanM.Application.BackgroundServices
{
    public class UserEventExportHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<UserEventExportHostedService> _logger;
        private const int _maxExportSize = 100;

        public UserEventExportHostedService(IServiceScopeFactory scopeFactory, ILogger<UserEventExportHostedService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using PeriodicTimer timer = new(TimeSpan.FromSeconds(30));
            try
            {
                while(await timer.WaitForNextTickAsync(cancellationToken))
                {
                    _logger.LogDebug("Exporting user events from timer");
                    await ExportAsync(_maxExportSize);
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogDebug("Graceful shutdown was requested - exporting main events.");
                await ExportAsync(int.MaxValue);
            }
        }

        private async Task ExportAsync(int exportSize)
        {
            using var scope = _scopeFactory.CreateScope();
            var userEventTracker = scope.ServiceProvider.GetRequiredService<UserEventTracker>();
            var userEventRepository = scope.ServiceProvider.GetRequiredService<IUserEventRepository>();

            var userEvents = userEventTracker.Emit(exportSize);
            await userEventRepository.SaveBatch(userEvents);
        }
    }
}
