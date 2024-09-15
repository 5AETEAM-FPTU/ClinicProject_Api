namespace Clinic.Configuration.Infrastructure.AspnetcoreIdentity;

/// summary
///     The AspNetCoreIdentityOption class is used to hold various database configuration settings.
/// summary
public sealed class AspNetCoreIdentityOption
{
    public PasswordOption Password { get; set; } = new();

    public LockoutOption Lockout { get; set; } = new();

    public UserOption User { get; set; } = new();

    public SignInOption SignIn { get; set; } = new();

    public sealed class PasswordOption
    {
        public bool RequireDigit { get; set; }

        public bool RequireLowercase { get; set; }

        public bool RequireNonAlphanumeric { get; set; }

        public bool RequireUppercase { get; set; }

        public int RequiredLength { get; set; }

        public int RequiredUniqueChars { get; set; }
    }

    public sealed class LockoutOption
    {
        public int DefaultLockoutTimeSpanInSecond { get; set; }

        public int MaxFailedAccessAttempts { get; set; }

        public bool AllowedForNewUsers { get; set; }
    }

    public sealed class UserOption
    {
        public string AllowedUserNameCharacters { get; set; }

        public bool RequireUniqueEmail { get; set; }
    }

    public sealed class SignInOption
    {
        public bool RequireConfirmedEmail { get; set; }

        public bool RequireConfirmedPhoneNumber { get; set; }
    }
}
