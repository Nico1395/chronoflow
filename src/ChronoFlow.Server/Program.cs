using NLog;
using NLog.Extensions.Logging;

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
            builder.Services.AddAuthorization();
            builder.Services.AddChronoFlow();
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                loggingBuilder.AddNLog(LogManager.Configuration);
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseChronoFlowEndpoints();
            app.MapControllers();
            app.UseCors(c =>
            {
                c.AllowAnyOrigin();
                c.AllowAnyHeader();
                c.AllowAnyMethod();
            });

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
