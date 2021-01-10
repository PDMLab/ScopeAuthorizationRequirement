using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FSharpx.Collections;
using Microsoft.AspNetCore.Authorization;

namespace ScopeAuthorizationRequirement
{
  public class ScopeAuthorizationRequirement : AuthorizationHandler<ScopeAuthorizationRequirement>,
    IAuthorizationRequirement
  {
    const string Scope = "scope";
    private const string Separator = " ";
    public IEnumerable<string> RequiredScopes { get; }

    public ScopeAuthorizationRequirement(NonEmptyList<string> requiredScopes)
    {
      if (requiredScopes == null)
        throw new ArgumentNullException($"{nameof(requiredScopes)} must contain at least one value.",
          nameof(requiredScopes));

      RequiredScopes = requiredScopes.AsEnumerable();
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
      ScopeAuthorizationRequirement requirement)
    {
      var scopeClaim = context.User?.Claims.FirstOrDefault(
        claim => string.Equals(claim.Type, Scope, StringComparison.OrdinalIgnoreCase));

      if (scopeClaim == null) return Task.CompletedTask;

      var scopes = scopeClaim.Value.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

      if (requirement.RequiredScopes.All(requiredScope => scopes.Contains(requiredScope)))
      {
        context.Succeed(requirement);
      }

      return Task.CompletedTask;
    }
  }
}