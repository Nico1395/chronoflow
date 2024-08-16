using NLog;

namespace ChronoFlow.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var logger = Logging.CreateLogger();

        try
        {
            logger.Debug("Starting application...");

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddChronoFlow();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseChronoFlowEndpoints();

            app.Run();
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Application ran into an unhandled exception.");
            throw;
        }
        finally
        {
            LogManager.Shutdown();
        }
    }
}
