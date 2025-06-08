using Microsoft.AspNetCore.Identity;
using ShelfApi.Domain.Common.Tools;

namespace ShelfApi.Domain.UserAggregate;

public sealed class Role : IdentityRole<long>
{
    private Role() { }

    public Role(short id, RoleName name) : base(name.ToString())
    {
        Id = id;

        NormalizedName = name.ToString().ToUpper();

        ConcurrencyStamp = Utils.GenerateNewConcurrencyStamp();
    }
}