using Microsoft.AspNetCore.Identity;

namespace ShelfApi.Modules.IdentityModule.Domain;

public sealed class User : IdentityUser<long>
{
    public const string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._-";

    private User() { }

    public User(long id, bool isAdmin, string username, string emailAddress)
    {
        Id = id;

        IsAdmin = isAdmin;

        UserName = username;
        NormalizedUserName = UserName.ToUpper();

        Email = emailAddress;
        NormalizedEmail = string.IsNullOrWhiteSpace(emailAddress) ? null : Email.ToUpper();
        EmailConfirmed = false;

        SecurityStamp = GenerateNewSecurityStamp();
        ConcurrencyStamp = Guid.NewGuid().ToString();

        TwoFactorEnabled = false;

        LockoutEnabled = true;
        AccessFailedCount = 0;
        LockoutEnd = null;

        CreatedAt = DateTime.UtcNow;
    }

    public bool IsAdmin { get; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }

    private static string GenerateNewSecurityStamp()
        => Guid.NewGuid().ToString();

    public void SetModifiedAt(DateTime modifiedAt)
    {
        if (modifiedAt <= CreatedAt)
            throw new ArgumentOutOfRangeException(nameof(modifiedAt));

        ModifiedAt = modifiedAt;
    }
}