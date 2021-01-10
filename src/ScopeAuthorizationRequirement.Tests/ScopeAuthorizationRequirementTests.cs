using System.Linq;
using System.Security.Claims;
using FSharpx.Collections;
using Microsoft.AspNetCore.Authorization;
using Xunit;

namespace ScopeAuthorizationRequirement.Tests
{
  public class ScopeAuthorizationRequirementTests
  {
    [Fact]
    public void SingleScopeRegistrationTest()
    {
      const string scope = "openid";
      var requirement = new ScopeAuthorizationRequirement(NonEmptyList.Create(scope));
      Assert.Equal(scope, requirement.RequiredScopes.ToList().First());
    }

    [Fact]
    public void MultiScopeRegistrationTest()
    {
      const string openIdScope = "openid";
      const string profileScope = "profile";
      var requirement = new ScopeAuthorizationRequirement(NonEmptyList.Create(openIdScope, profileScope));
      Assert.Equal(openIdScope, requirement.RequiredScopes.ToList().First());
      Assert.Equal(profileScope, requirement.RequiredScopes.ToList()[1]);
    }

    [Fact]
    public void SingleScopeRequirementWithSingleScopeTest()
    {
      const string openIdScope = "openid";
      var requirement = new ScopeAuthorizationRequirement(NonEmptyList.Create(openIdScope));
      var context = new AuthorizationHandlerContext(new[] {requirement},
        new ClaimsPrincipal(new ClaimsIdentity(new[] {new Claim("scope", "openid")})), null);
      requirement.HandleAsync(context);
      Assert.True(context.HasSucceeded);
    }

    [Fact]
    public void SingleScopeRequirementWithMultiScopeTest()
    {
      const string openIdScope = "openid";
      var requirement = new ScopeAuthorizationRequirement(NonEmptyList.Create(openIdScope));
      var context = new AuthorizationHandlerContext(new[] {requirement},
        new ClaimsPrincipal(new ClaimsIdentity(new[] {new Claim("scope", "openid profile")})), null);
      requirement.HandleAsync(context);
      Assert.True(context.HasSucceeded);
    }

    [Fact]
    public void MultiScopeRequirementWithMultiScopeTest()
    {
      const string openIdScope = "openid";
      const string profileScope = "profile";
      var requirement = new ScopeAuthorizationRequirement(NonEmptyList.Create(openIdScope, profileScope));
      var context = new AuthorizationHandlerContext(new[] {requirement},
        new ClaimsPrincipal(new ClaimsIdentity(new[] {new Claim("scope", "openid profile")})), null);
      requirement.HandleAsync(context);
      Assert.True(context.HasSucceeded);
    }
  }
}