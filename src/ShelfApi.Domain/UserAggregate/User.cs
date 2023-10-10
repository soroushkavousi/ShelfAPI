using Microsoft.AspNetCore.Identity;
using ShelfApi.Domain.Common;

namespace ShelfApi.Domain.UserAggregate;

public class User : IdentityUser<ulong>
{
    public const string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._-";

    private User() { }

    public User(ulong id, bool isAdmin, string username, string emailAddress)
    {
        Id = id;

        IsAdmin = isAdmin;

        UserName = username;
        NormalizedUserName = UserName.ToUpper();

        Email = emailAddress;
        NormalizedEmail = string.IsNullOrWhiteSpace(emailAddress) ? null : Email.ToUpper();
        EmailConfirmed = false;

        SecurityStamp = GenerateNewSecurityStamp();
        ConcurrencyStamp = Utils.GenerateNewConcurrencyStamp();

        TwoFactorEnabled = false;

        LockoutEnabled = true;
        AccessFailedCount = 0;
        LockoutEnd = null;

        CreatedAt = DateTime.UtcNow;
    }

    public bool IsAdmin { get; }
    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }

    private string GenerateNewSecurityStamp()
        => Guid.NewGuid().ToString();

    public void SetModifiedAt(DateTime modifiedAt)
    {
        if (modifiedAt <= CreatedAt)
            throw new ArgumentOutOfRangeException(nameof(modifiedAt));

        ModifiedAt = modifiedAt;
    }
}
