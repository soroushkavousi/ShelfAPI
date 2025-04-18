using Microsoft.AspNetCore.Identity;
using ShelfApi.Domain.Common.Tools;

namespace ShelfApi.Domain.UserAggregate;

public class Role : IdentityRole<long>
{
    private Role() { }

    public Role(RoleName name) : base(name.ToString())
    {
        NormalizedName = name.ToString().ToUpper();

        ConcurrencyStamp = Utils.GenerateNewConcurrencyStamp();
    }
}