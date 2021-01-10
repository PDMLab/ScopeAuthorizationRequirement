using FSharpx.Collections;
using Microsoft.AspNetCore.Authorization;

namespace ScopeAuthorizationRequirement
{
  public static class ScopeAuthorizationRequirementExtensions
  {
    public static AuthorizationPolicyBuilder RequireScope(
      this AuthorizationPolicyBuilder authorizationPolicyBuilder,
      string requiredScope)
    {
      authorizationPolicyBuilder.RequireScopes(NonEmptyList.Create(requiredScope));
      return authorizationPolicyBuilder;
    }
 
    public static AuthorizationPolicyBuilder RequireScopes(
      this AuthorizationPolicyBuilder authorizationPolicyBuilder,
      NonEmptyList<string> requiredScopes)
    {
      authorizationPolicyBuilder.AddRequirements(new ScopeAuthorizationRequirement(requiredScopes));
      return authorizationPolicyBuilder;
    }
  }
}