// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Security.Claims;

namespace MyApp.Extensions
{
  public static class ClaimsPrincipalExtensions
  {
    public static bool HasClaim(this ClaimsPrincipal claimsPrincipal, string type, string value)
    {
      if (!claimsPrincipal.Identity.IsAuthenticated)
        throw new InvalidOperationException("Claims principal is not authenticated.");

      Claim claim = claimsPrincipal.Claims.FirstOrDefault(c => string.Equals(c.Type, type, StringComparison.OrdinalIgnoreCase));

      if (claim == null)
        throw new InvalidOperationException($"Claims principal doesn't have {type} claim.");

      return string.Equals(claim.Value, value, StringComparison.OrdinalIgnoreCase);
    }

    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
      if (!claimsPrincipal.Identity.IsAuthenticated)
        throw new InvalidOperationException("Claims principal is not authenticated.");

      Claim claim = claimsPrincipal.Claims.FirstOrDefault(c => string.Equals(c.Type, ClaimTypes.NameIdentifier, StringComparison.OrdinalIgnoreCase));

      if (claim == null)
        throw new InvalidOperationException("Claims principal doesn't have NameIdentifier claim.");

      if (!int.TryParse(claim.Value, out int userId))
        throw new InvalidOperationException("Claims principal's NameIdentifier claim contains non-integer value.");

      return userId;
    }
  }
}