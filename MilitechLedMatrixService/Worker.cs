namespace MilitechLedMatrixService;

using MilitechLedMatrixService.Graphics;

public class Worker(ILogger<Worker> logger, HexDisplay hexDisplay) 
    : BackgroundService
{
    private ILogger<Worker> _logger = logger;
    private HexDisplay _hexDisplay = hexDisplay;
    private HexDigitAnimation _animation = new(hexDisplay);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _animation.Update();
                _animation.Draw();
                
                await Task.Delay(12, stoppingToken);
            }
        }
        catch (OperationCanceledException)
        {
            Environment.Exit(0);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "{Message}", e.Message);
            Environment.Exit(1);
        }
    }
}