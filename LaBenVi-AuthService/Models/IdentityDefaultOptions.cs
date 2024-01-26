namespace LaBenVi_AuthService.Models
{
    public class IdentityDefaultOptions
    {
        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireUniqueChars { get; set; }
        public int LockoutDefaultLockoutTimeSpanInMinutes { get; set; }
        public int LockoutMaxFailedAccessAttempt { get; set; }
        public bool LockoutAllowedForNewUsers { get; set; }
        public bool UserRequireUniqueEmail { get; set; }
        public bool SignInRequireConfirmEmail { get; set; }
        public bool CookieHttpOnly { get; set; }
        public int CookieExpiration { get; set; }
        public int ExpireTimeSpan { get; set; }
        public string? LoginPath { get; set; }
        public string? LogoutPath { get; set; }
        public string? AccessDeniedPath { get; set; }
        public bool SlidingExpiration { get; set; }

    
    }
}
