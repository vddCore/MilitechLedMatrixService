namespace MilitechLedMatrixService;

using MilitechLedMatrixService.Graphics;

public class Worker(HexDisplay hexDisplay) 
{
    private readonly HexDigitAnimation _animation = new(hexDisplay);
    
    public async Task ExecuteAsync(CancellationToken stoppingToken)
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
            Environment.Exit(1);
        }
    }
}