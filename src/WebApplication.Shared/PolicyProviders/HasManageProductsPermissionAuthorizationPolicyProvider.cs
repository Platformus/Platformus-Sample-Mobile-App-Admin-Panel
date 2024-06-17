// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authorization;

namespace WebApplication
{
  public class HasManageProductsPermissionAuthorizationPolicyProvider : Platformus.Core.IAuthorizationPolicyProvider
  {
    public string Name => Policies.HasManageProductsPermission;

    public AuthorizationPolicy GetAuthorizationPolicy()
    {
      AuthorizationPolicyBuilder authorizationPolicyBuilder = new AuthorizationPolicyBuilder();

      authorizationPolicyBuilder.RequireAssertion(context =>
        {
          return context.User.HasClaim(Platformus.Core.PlatformusClaimTypes.Permission, Permissions.ManageProducts) || context.User.HasClaim(Platformus.Core.PlatformusClaimTypes.Permission, Platformus.Core.Permissions.DoAnything);
        }
      );

      return authorizationPolicyBuilder.Build();
    }
  }
}