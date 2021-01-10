# ScopeAuthorizationRequirement
This package provides a `scope` claim authorization requirement for ASP.NET Core.

It handles space delimited `scope` claims according to https://tools.ietf.org/html/rfc6749#section-3.3.

## Installation

```bash
dotnet add package ScopeAuthorizationRequirement
```

## Usage

### Single scope required

```csharp
services.AddAuthorization(options =>
{
    options.AddPolicy("openid", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireScope("openid");
    });
});
```

### Multiple scopes required

```csharp
services.AddAuthorization(options =>
{
    options.AddPolicy("openid+profile", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireScopes(NonEmptyList.Create("openid", "profile));
    });
});
```