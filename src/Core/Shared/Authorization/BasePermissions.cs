using System.Collections.ObjectModel;

namespace Belsio.Erp.Shared.Authorization;

public static class BaseAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class BaseResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);
}

public static class BasePermissions
{
    private static readonly FSHPermission[] _all = new FSHPermission[]
    {
        new("View Dashboard", BaseAction.View, BaseResource.Dashboard),
        new("View Hangfire", BaseAction.View, BaseResource.Hangfire),
        new("View Users", BaseAction.View, BaseResource.Users),
        new("Search Users", BaseAction.Search, BaseResource.Users),
        new("Create Users", BaseAction.Create, BaseResource.Users),
        new("Update Users", BaseAction.Update, BaseResource.Users),
        new("Delete Users", BaseAction.Delete, BaseResource.Users),
        new("Export Users", BaseAction.Export, BaseResource.Users),
        new("View UserRoles", BaseAction.View, BaseResource.UserRoles),
        new("Update UserRoles", BaseAction.Update, BaseResource.UserRoles),
        new("View Roles", BaseAction.View, BaseResource.Roles),
        new("Create Roles", BaseAction.Create, BaseResource.Roles),
        new("Update Roles", BaseAction.Update, BaseResource.Roles),
        new("Delete Roles", BaseAction.Delete, BaseResource.Roles),
        new("View RoleClaims", BaseAction.View, BaseResource.RoleClaims),
        new("Update RoleClaims", BaseAction.Update, BaseResource.RoleClaims),
        new("View Tenants", BaseAction.View, BaseResource.Tenants, IsRoot: true),
        new("Create Tenants", BaseAction.Create, BaseResource.Tenants, IsRoot: true),
        new("Update Tenants", BaseAction.Update, BaseResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", BaseAction.UpgradeSubscription, BaseResource.Tenants, IsRoot: true)
    };

    public static IReadOnlyList<FSHPermission> All { get; } = new ReadOnlyCollection<FSHPermission>(_all);
    public static IReadOnlyList<FSHPermission> Root { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Admin { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Basic { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record FSHPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
