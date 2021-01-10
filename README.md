# ScopeAuthorizationRequirement
This package provides a `scope` claim authorization requirement for ASP.NET Core.

It handles multiple space delimited `scope` claims according to https://tools.ietf.org/html/rfc6749#section-3.3 (which the ASP.NET Core `RequireClaim` Requirement does not, hence can't be used e.g. with AWS Cognito when using multiple scopes).

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
