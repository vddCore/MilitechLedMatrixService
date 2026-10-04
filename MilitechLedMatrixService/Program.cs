namespace MilitechLedMatrixService;

using MilitechLedMatrixService.Graphics;

public class Program
{
    private static HexDisplay _hexDisplay = new();
    public static async Task Main(string[] args)
    {
        var cts = new CancellationTokenSource();
        var worker = new Worker(_hexDisplay);
        await worker.ExecuteAsync(cts.Token);
    }
}