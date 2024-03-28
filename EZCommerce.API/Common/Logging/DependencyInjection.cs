using Serilog;

public static class DependencyInjection
{
    public static void AddLoggingConfiguration(this IServiceCollection services)
    {
        var logFolderPath = Path.Combine(AppContext.BaseDirectory, "Logs");

        if (!Directory.Exists(logFolderPath))
        {
            Directory.CreateDirectory(logFolderPath);
        }

        var logFilePath = Path.Combine(logFolderPath, $"log-.txt");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Async(a =>
            {
                a.File(logFilePath, rollingInterval: RollingInterval.Day, retainedFileTimeLimit: TimeSpan.FromDays(7));
            })
            .CreateLogger();
    }
}