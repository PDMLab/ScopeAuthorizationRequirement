using System.Linq;
using FSharpx.Collections;
using Microsoft.AspNetCore.Authorization;
using Xunit;

namespace ScopeAuthorizationRequirement.Tests
{
  public class ScopeAuthorizationRequirementExtensionTests
  {
    [Fact]
    public void SingleScopeRegistrationTest()
    {
      const string openIdScope = "openid";
      var builder = new AuthorizationPolicyBuilder();
      builder.RequireScope(openIdScope);
      var requiredScopes = ((ScopeAuthorizationRequirement) builder.Requirements.ToList()[0]).RequiredScopes.ToList();
      Assert.Single(requiredScopes, openIdScope);
    }

    [Fact]
    public void MultiScopeRegistrationTest()
    {
      const string openIdScope = "openid";
      const string profileScope = "profile";
      var builder = new AuthorizationPolicyBuilder();
      builder.RequireScopes(NonEmptyList.Create(openIdScope, profileScope));
      var requiredScopes = ((ScopeAuthorizationRequirement) builder.Requirements.ToList()[0]).RequiredScopes.ToList();
      Assert.Contains(openIdScope, requiredScopes);
      Assert.Contains(profileScope, requiredScopes);
    }
  }
}