using Microsoft.AspNetCore.Identity;
using ShelfApi.Domain.Common;

namespace ShelfApi.Domain.UserAggregate;

public class Role : IdentityRole<ulong>
{
    private Role() { }

    public Role(RoleName name) : base(name.ToString())
    {
        NormalizedName = name.ToString().ToUpper();

        ConcurrencyStamp = Utils.GenerateNewConcurrencyStamp();

        CreatedAt = DateTime.UtcNow;
    }

    public DateTime CreatedAt { get; }
    public DateTime? ModifiedAt { get; private set; }
}