using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Platformus.Core.Data.Entities;

namespace MyApp
{
  public class AccessTokenGenerator
  {
    private string secret;

    public AccessTokenGenerator(IOptions<JwtOptions> jwtOptions)
    {
      this.secret = jwtOptions.Value?.Secret;
    }

    public string Generate(User user)
    {
      SigningCredentials signingCredentials = new SigningCredentials(this.CreateSecurityKey(), SecurityAlgorithms.HmacSha256);
      JwtSecurityToken jwt = new JwtSecurityToken(
        claims: this.GetUserClaims(user),
        expires: DateTime.Now.AddMinutes(5),
        signingCredentials: signingCredentials
      );

      return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    public SecurityKey CreateSecurityKey()
    {
      return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
    }

    private IEnumerable<Claim> GetUserClaims(User user)
    {
      List<Claim> claims = new List<Claim>();

      claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
      claims.AddRange(this.GetUserRoleClaims(user));
      return claims;
    }

    private IEnumerable<Claim> GetUserRoleClaims(User user)
    {
      List<Claim> claims = new List<Claim>();

      foreach (UserRole userRole in user.UserRoles)
      {
        if (!string.IsNullOrEmpty(userRole.Role.Code))
          claims.Add(new Claim(ClaimTypes.Role, userRole.Role.Code));

        claims.AddRange(this.GetUserPermissionClaims(userRole.Role));
      }

      return claims;
    }

    private IEnumerable<Claim> GetUserPermissionClaims(Role role)
    {
      List<Claim> claims = new List<Claim>();

      foreach (RolePermission rolePermission in role.RolePermissions)
        claims.Add(new Claim(ClaimType.Permission, rolePermission.Permission.Code));

      return claims;
    }
  }
}