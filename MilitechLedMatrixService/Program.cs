namespace MilitechLedMatrixService;

using MilitechLedMatrixService.Graphics;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services
            .AddSingleton(new HexDisplay())
            .AddHostedService<Worker>()
            .AddWindowsService(options =>
            {
                options.ServiceName = "Militech Key Negotiation Service";
            });

        var host = builder.Build();
        host.Run();
    }
}