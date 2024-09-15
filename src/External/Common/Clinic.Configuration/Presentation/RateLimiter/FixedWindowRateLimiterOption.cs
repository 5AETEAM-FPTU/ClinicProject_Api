namespace Clinic.Configuration.Presentation.RateLimiter;

/// <summary>
///     FixedWindowRateLimiterOption class is used to hold various swagger configuration settings
/// </summary>
public sealed class FixedWindowRateLimiterOption
{
    public RemoteIpAddressOption RemoteIpAddress { get; set; } = new();

    public sealed class RemoteIpAddressOption
    {
        public int PermitLimit { get; set; }

        public int QueueProcessingOrder { get; set; }

        public int QueueLimit { get; set; }

        public int Window { get; set; }

        public bool AutoReplenishment { get; set; }
    }
}
