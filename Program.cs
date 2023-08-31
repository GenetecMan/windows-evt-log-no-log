using System.Runtime.InteropServices;

namespace WindowsEventLog;

public class Program
{
    public static void Main(string[] args)
    {
        IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    logging.AddEventLog(new Microsoft.Extensions.Logging.EventLog.EventLogSettings()
                    {
                        LogName = "Application",
                        SourceName = "Monitor"
                    });
                }
            })
            .ConfigureServices(services =>
            {
                services.AddHostedService<Worker>();
            })
            .Build();

        host.Run();
    }
}