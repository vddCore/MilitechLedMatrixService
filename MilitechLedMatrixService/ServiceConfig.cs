namespace MilitechLedMatrixService;

public static class ServiceConfig
{
    public static int IntervalBase
    {
        get
        {
            var baseEnv = Environment.GetEnvironmentVariable("MILITECH_KEY_NEGOTIATION_INTERVAL_BASE_SECONDS");
            if (baseEnv != null)
            {
                if (int.TryParse(baseEnv, out var intervalBase))
                    return intervalBase;
                
            }
            
            return 15;
        }
    }

    public static int IntervalMinimumFactor
    {
        get
        {
            var minEnv = Environment.GetEnvironmentVariable("MILITECH_KEY_NEGOTIATION_INTERVAL_MIN_FACTOR");
            if (minEnv != null)
            {
                if (int.TryParse(minEnv, out var intervalMinFactor))
                    return intervalMinFactor;
            }
            
            return 1;
        }
    }

    public static int IntervalMaximumFactor
    {
        get
        {
            var maxEnv = Environment.GetEnvironmentVariable("MILITECH_KEY_NEGOTIATION_INTERVAL_MAX_FACTOR");
            if (maxEnv != null)
            {
                if (int.TryParse(maxEnv, out var intervalMaxFactor))
                    return intervalMaxFactor;
            }
            
            return 4;
        }
    }
}