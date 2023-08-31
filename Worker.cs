using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsEventLog;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // This logs correctly.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var log = new EventLog("Application")
                {
                    Source = "Test123"
                };

                log.WriteEntry("Error 123");
            }

            // This line simply does not show up!
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            // This line shows up in the event log under SourceName ".NET Runtime".
            // The same happens for LogError.
            _logger.LogWarning("Worker running at: {time}", DateTimeOffset.Now);

            await Task.Delay(1000, stoppingToken);
        }
    }
}