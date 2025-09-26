using Microsoft.AspNetCore.Identity;

namespace ShelfApi.IdentityModule.Domain;

public sealed class Role : IdentityRole<long>
{
    private Role() { }

    public Role(short id, RoleName name) : base(name.ToString())
    {
        Id = id;

        NormalizedName = name.ToString().ToUpper();

        ConcurrencyStamp = Guid.NewGuid().ToString();
    }
}