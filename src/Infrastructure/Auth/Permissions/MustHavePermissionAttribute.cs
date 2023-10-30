using Belsio.Erp.Shared.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Belsio.Erp.Infrastructure.Auth.Permissions;

public class MustHavePermissionAttribute : AuthorizeAttribute
{
    public MustHavePermissionAttribute(string action, string resource) =>
        Policy = FSHPermission.NameFor(action, resource);
}